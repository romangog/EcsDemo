using UnityEngine;
using Leopotam.Ecs;

public class ProjectileSpeedLevelSystem : IEcsRunSystem
{
    EcsFilter<ProjectileShotRequest, MoveForwardComponent> _shotProjectilesFilter;

    private WeaponUpgradeLevels _weaponUpgradeLevels = null;

    public void Run()
    {
        foreach (var id in _shotProjectilesFilter)
        {
            ref var entity = ref _shotProjectilesFilter.GetEntity(id);
            ref var moveForward = ref _shotProjectilesFilter.Get2(id);
            moveForward.Speed = _weaponUpgradeLevels.GetProjectileSpeedFromLevel();
            Debug.Log("SetSpeed " + moveForward.Speed);
        }
    }
}

