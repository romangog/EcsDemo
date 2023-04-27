using Leopotam.Ecs;

public class FragmentationFinalDeathSystem : IEcsRunSystem
{
    private EcsFilter<DeathRequest, ProjectileFragmentTag> _dyingFragmentation;
    private EcsFilter<DeathRequest, ProjectileTag> _dyingProjectiles;

    private WeaponUpgradeLevels _weaponUpgrades;

    public void Run()
    {
        foreach (var i in _dyingFragmentation)
        {
            ref var projectileEntity = ref _dyingFragmentation.GetEntity(i);
            projectileEntity.Get<ProjectileFinalDeathRequest>();
        }

        if (_weaponUpgrades.FragmentationLevel == 0)
        {
            foreach (var i in _dyingProjectiles)
            {
                ref var projectileEntity = ref _dyingProjectiles.GetEntity(i);
                projectileEntity.Get<ProjectileFinalDeathRequest>();
            }
        }
    }
}


