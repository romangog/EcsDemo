using Leopotam.Ecs;

public class ProjectilePenetrationLevelSystem : IEcsRunSystem
{
    private EcsFilter<ProjectileShotRequest, ProjectilePenetrationComponent> _projectileShotFilter;

    private WeaponUpgradeLevels _weaponUpgrade = null;

    public void Run()
    {
        foreach (var i in _projectileShotFilter)
        {
            ref var entity = ref _projectileShotFilter.GetEntity(i);
            ref var penetration = ref _projectileShotFilter.Get2(i);

            penetration.PenetrationsLeft = _weaponUpgrade.PenetrationLevel;
        }
    }
}
