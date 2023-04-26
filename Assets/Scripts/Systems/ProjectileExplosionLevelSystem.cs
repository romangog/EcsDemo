using Leopotam.Ecs;
using UnityEngine;

public class ProjectileExplosionLevelSystem : IEcsRunSystem
{
    // Добавить компоненту авто-аима, убрать из системы проверку на аим
    private EcsFilter<ProjectileTag, ProjectileFinalDeathRequest, TransformComponent> _projectileFilter;
    private EcsFilter<EnemyTag, TransformComponent>.Exclude<DeadTag> _enemiesFilter;

    private WeaponUpgradeLevels _weaponUpgrades = null;
    private Prefabs _prefabs;

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
            float particlularExplosionRange = (isFragment) ? explosionRange * 0.5f : explosionRange ;
            float particlularExplosionDamage = (isFragment) ? explosionDamage * 0.5f : explosionDamage ;
            foreach (var j in _enemiesFilter)
            {
                ref var enemyEntity = ref _enemiesFilter.GetEntity(j);
                ref var transform = ref _enemiesFilter.Get2(j);

                ref var accumulativeDamage = ref enemyEntity.Get<AccumulativeDamageComponent>();

                if (Vector2.Distance(transform.Transform.position, projectileTransform.Transform.position) <= particlularExplosionRange)
                {
                    accumulativeDamage.Damage += particlularExplosionDamage;
                }
            }

            // Spawn explosion fx
            GameObject explosionFx = GameObject.Instantiate(
                _prefabs.ExplosionPrefab,
                projectileTransform.Transform.position,
                Quaternion.Euler(0, 0, UnityEngine.Random.Range(180f, 180f)));
            explosionFx.transform.localScale = Vector3.one * _weaponUpgrades.GetProjectileExplosionFxScaleFromLevel();
            GameObject.Destroy(explosionFx, 0.2f);
        }
    }
}


