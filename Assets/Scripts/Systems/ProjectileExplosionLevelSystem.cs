﻿using Leopotam.Ecs;
using UnityEngine;

public class ProjectileExplosionLevelSystem : IEcsRunSystem
{
    // Добавить компоненту авто-аима, убрать из системы проверку на аим
    private EcsFilter<ProjectileComponent, ProjectileFinalDeathRequest, TransformComponent> _projectileFilter;
    private EcsFilter<EnemyTag, TransformComponent>.Exclude<DeadTag> _enemiesFilter;

    private WeaponUpgradeLevels _weaponUpgrades = null;
    private Prefabs _prefabs;
    private EcsWorld _world;
    private GameSettings _gameSettings;

    public void Run()
    {
        if (_weaponUpgrades.ExplosionLevel == 0) return;
        float explosionRange = _weaponUpgrades.GetProjectileExplosionRangeFromLevel();
        float explosionDamage = _weaponUpgrades.GetProjectileExplosionDamageFromLevel();
        foreach (var i in _projectileFilter)
        {

            ref var projectileEntity = ref _projectileFilter.GetEntity(i);
            ref var projectileTransform = ref _projectileFilter.Get3(i);

            bool isFragment = projectileEntity.Has<ProjectileFragmentTag>();
            float particlularExplosionRange = (isFragment) ? explosionRange * 0.5f : explosionRange;
            float particlularExplosionDamage = (isFragment) ? explosionDamage * 0.5f : explosionDamage;

            var explosionEntity = _world.NewEntity();
            explosionEntity.Get<ExplosionTag>();
            explosionEntity.Get<OnSpawnRequest>();
            ref var explosionAffectedTargets = ref explosionEntity.Get<AffectedTargetsComponent>();
            explosionAffectedTargets.Targets = new System.Collections.Generic.List<EcsEntity>();

            foreach (var j in _enemiesFilter)
            {
                ref var enemyEntity = ref _enemiesFilter.GetEntity(j);
                ref var transform = ref _enemiesFilter.Get2(j);


                if (Vector2.Distance(transform.Transform.position, projectileTransform.Transform.position) <= particlularExplosionRange)
                {
                    enemyEntity.Get<AccumulativeDamageComponent>().Damage += particlularExplosionDamage;
                    explosionAffectedTargets.Targets.Add(enemyEntity);
                }
            }


            // Spawn explosion fx
            Explosion explosionGO = GameObject.Instantiate(
                _prefabs.ExplosionPrefab,
                projectileTransform.Transform.position,
                Quaternion.Euler(0, 0, UnityEngine.Random.Range(180f, 180f)));
            explosionGO.transform.localScale = Vector3.one * _weaponUpgrades.GetProjectileExplosionFxScaleFromLevel();

            explosionEntity.Get<SpriteRendererComponent>() = explosionGO.SpriteRenderer;
            explosionEntity.Get<ElementalParticlesComponent>()= explosionGO.ElementalParticles;
            explosionEntity.Get<SetBaseColorRequest>().BaseColor = _gameSettings.BlackColor;

            GameObject.Destroy(explosionGO.gameObject, 0.4f);
        }
    }
}



