using Leopotam.Ecs;

public class ProjectileFragmentationLevelSystem : IEcsRunSystem
{
    private EcsFilter<ProjectileHitRequest> _projectileShotFilter;

    private WeaponUpgradeLevels _weaponUpgrade = null;

    public void Run()
    {
        foreach (var i in _projectileShotFilter)
        {
            ref var entity = ref _projectileShotFilter.GetEntity(i);
        }
    }
}


