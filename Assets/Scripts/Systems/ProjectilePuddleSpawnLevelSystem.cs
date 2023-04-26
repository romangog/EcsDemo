using Leopotam.Ecs;
using UnityEngine;

public class ProjectilePuddleSpawnLevelSystem : IEcsRunSystem
{
    private EcsFilter<ProjectileTag, ProjectileFinalDeathRequest, TransformComponent> _projectilesFilter;

    private WeaponUpgradeLevels _weaponUpgrades = null;
    private Prefabs _prefabs;
    private EcsWorld _world;

    public void Run()
    {
        if (_weaponUpgrades.PuddleLevel == 0) return;
        foreach (var i in _projectilesFilter)
        {
            ref var projectileEntity= ref _projectilesFilter.GetEntity(i);
            ref var projectileTransform = ref _projectilesFilter.Get3(i);

            // Spawn a puddle
            var entity = _world.NewEntity();

            entity.Get<PuddleTag>();
            ref var puddleData = ref entity.Get<PuddleData>();

            float projectileMod = 1f;
            if (projectileEntity.Has<ProjectileFragmentTag>())
                projectileMod = 0.5f;

            puddleData.Radius = _weaponUpgrades.GetPuddleRadiusFromLevel() * projectileMod;
            puddleData.Efficiency = _weaponUpgrades.GetPuddleEfficiencyFromLevel() * projectileMod;
            puddleData.LifeTimer.Set(_weaponUpgrades.GetPuddleLifeTimeFromLevel() * projectileMod);

            GameObject puddleGO = GameObject.Instantiate(_prefabs.PuddlePrefab, projectileTransform.Transform.position, Quaternion.Euler(0, 0, UnityEngine.Random.Range(0, 4) * 90f));
            puddleGO.transform.localScale = Vector3.one * puddleData.Radius * 2f;

            if (UnityEngine.Random.Range(0, 2) == 1)
            {
                puddleGO.transform.localScale = puddleGO.transform.localScale.SetX(-puddleGO.transform.localScale.x);
            }
            if (UnityEngine.Random.Range(0, 2) == 1)
            {
                puddleGO.transform.localScale = puddleGO.transform.localScale.SetY(-puddleGO.transform.localScale.y);
            }
            entity.Get<GameObjectComponent>().GameObject = puddleGO;
            entity.Get<TransformComponent>().Transform = puddleGO.transform;
        }
    }
}


