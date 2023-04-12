using Leopotam.Ecs;

public class ProjectileDamageLevelSystem : IEcsRunSystem
{
    private EcsFilter<ProjectileShotRequest> _projectileShotFilter;

    private WeaponUpgradeLevels _weaponUpgradeLevels = null;

    public void Run()
    {
        foreach (var i in _projectileShotFilter)
        {
            ref var entity = ref _projectileShotFilter.GetEntity(i);

            entity.Get<ProjectileDamageComponent>().Damage = _weaponUpgradeLevels.GetProjectileDamageFromLevel();
        }
    }
}
