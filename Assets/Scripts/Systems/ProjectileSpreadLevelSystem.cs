using Leopotam.Ecs;
using UnityEngine;

public class ProjectileSpreadLevelSystem : IEcsRunSystem
{
    private EcsFilter<ProjectileShotRequest, MoveForwardComponent, TransformComponent> _projectileShotFilter;

    private WeaponUpgradeLevels _weaponUpgrade = null;

    public void Run()
    {
        foreach (var i in _projectileShotFilter)
        {
            ref var entity = ref _projectileShotFilter.GetEntity(i);
            ref var moveForward = ref _projectileShotFilter.Get2(i);
            ref var transform = ref _projectileShotFilter.Get3(i);

            float angle = 0f;

            if (entity.Has<ProjectileFragmentTag>())
            {
                moveForward.Speed *= 0.75f;
                angle = UnityEngine.Random.Range(-180f, 180) * (_weaponUpgrade.FragmentationLevel / 3f);
            }
            else
            {
                angle = _weaponUpgrade.GetProjectileSpreadFromLevel();
            }

            transform.Transform.rotation = Quaternion.Euler(0, 0, angle) * Quaternion.LookRotation(Vector3.forward, moveForward.Direction);
            moveForward.Direction = new Vector2(transform.Transform.up.x, transform.Transform.up.y);
            transform.Transform.Translate(transform.Transform.up * 0.5f, Space.World);
        }
    }
}
