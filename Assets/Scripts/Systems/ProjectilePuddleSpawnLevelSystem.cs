using Leopotam.Ecs;
using UnityEngine;

public class ProjectilePuddleSpawnLevelSystem : IEcsRunSystem
{
    private EcsFilter<ProjectileComponent, ProjectileFinalDeathRequest, TransformComponent> _projectilesFilter;

    private WeaponUpgradeLevels _weaponUpgrades = null;
    private Prefabs _prefabs;
    private EcsWorld _world;
    private GameSettings _gameSetting;

    public void Run()
    {
        if (_weaponUpgrades.PuddleLevel == 0) return;
        foreach (var i in _projectilesFilter)
        {
            ref var projectileEntity = ref _projectilesFilter.GetEntity(i);
            ref var projectileTransform = ref _projectilesFilter.Get3(i);

            // Spawn a puddle
            var entity = _world.NewEntity();

            entity.Get<PuddleTag>();
            entity.Get<OnSpawnRequest>();
            ref var puddleData = ref entity.Get<PuddleData>();

            float projectileMod = 1f;
            if (projectileEntity.Has<ProjectileFragmentTag>())
                projectileMod = 0.5f;

            puddleData.Radius = _weaponUpgrades.GetPuddleRadiusFromLevel() * projectileMod;
            puddleData.Efficiency = _weaponUpgrades.GetPuddleEfficiencyFromLevel() * projectileMod;
            puddleData.LifeTimer.Set(_weaponUpgrades.GetPuddleLifeTimeFromLevel() * projectileMod);

            Puddle puddle = GameObject.Instantiate(_prefabs.PuddlePrefab, projectileTransform.Transform.position, Quaternion.Euler(0, 0, UnityEngine.Random.Range(0, 4) * 90f));
            puddle.transform.localScale = Vector3.one * puddleData.Radius * 2f;

            if (UnityEngine.Random.Range(0, 2) == 1)
            {
                puddle.transform.localScale = puddle.transform.localScale.SetX(-puddle.transform.localScale.x);
            }
            if (UnityEngine.Random.Range(0, 2) == 1)
            {
                puddle.transform.localScale = puddle.transform.localScale.SetY(-puddle.transform.localScale.y);
            }
            entity.Get<GameObjectComponent>().GameObject = puddle.gameObject;
            entity.Get<TransformComponent>().Transform = puddle.transform;
            entity.Get<ElementalParticlesComponent>() = puddle.ElementalParticles;
            entity.Get<SpriteRendererComponent>() = puddle.SpriteRenderer;
            entity.Get<PuddleAffectedTargetsComponent>().AffectedTargets = new System.Collections.Generic.List<EcsEntity>();
            entity.Get<SetBaseColorRequest>().BaseColor = _gameSetting.BlackColor;
        }
    }
}



