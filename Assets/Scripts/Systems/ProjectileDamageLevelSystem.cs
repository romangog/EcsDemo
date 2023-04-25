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

            ref var damage = ref entity.Get<ProjectileDamageComponent>();
            damage.Damage = _weaponUpgradeLevels.GetProjectileDamageFromLevel();

            if (entity.Has<ProjectileFragmentTag>())
            {
                damage.Damage *= 0.5f;
            }
        }
    }
}
