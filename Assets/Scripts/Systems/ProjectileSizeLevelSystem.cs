using Leopotam.Ecs;
using UnityEngine;

public class ProjectileSizeLevelSystem : IEcsRunSystem
{
    private EcsFilter<ProjectileShotRequest, TransformComponent> _projectileShotFilter;

    private WeaponUpgradeLevels _weaponUpgrade = null;

    public void Run()
    {
        foreach (var i in _projectileShotFilter)
        {
            ref var entity = ref _projectileShotFilter.GetEntity(i);
            ref var transform = ref _projectileShotFilter.Get2(i);

            ref var projectileSize = ref entity.Get<ProjectileSizeComponent>();

            projectileSize.Size = _weaponUpgrade.GetProjectileSizeFromLevel();

            if (entity.Has<ProjectileFragmentTag>())
            {
                projectileSize.Size *= 0.5f;
            }

            transform.Transform.localScale = Vector3.one * projectileSize.Size;

        }
    }
}