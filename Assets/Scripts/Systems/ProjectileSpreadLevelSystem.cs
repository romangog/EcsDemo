using Leopotam.Ecs;
using UnityEngine;

public class ProjectileSpreadLevelSystem : IEcsRunSystem
{
    private EcsFilter<ProjectileShotRequest, MoveForwardComponent, TransformComponent> _projectileShotFilter;

    private WeaponUpgradeLevels _weaponUpgradeLevels = null;

    public void Run()
    {
        foreach (var i in _projectileShotFilter)
        {
            ref var entity = ref _projectileShotFilter.GetEntity(i);
            ref var shotRequest = ref _projectileShotFilter.Get1(i);
            ref var moveForward = ref _projectileShotFilter.Get2(i);
            ref var transform = ref _projectileShotFilter.Get3(i);
            Debug.Log("Set Spread");
            float angle = _weaponUpgradeLevels.GetProjectileSpreadFromLevel();
            transform.Transform.rotation = Quaternion.Euler(0,0, angle) * Quaternion.LookRotation(Vector3.forward, moveForward.Direction);
            moveForward.Direction = new Vector2(transform.Transform.up.x, transform.Transform.up.y);
        }
    }
}