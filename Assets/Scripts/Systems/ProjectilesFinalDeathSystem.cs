using Leopotam.Ecs;

public class ProjectilesFinalDeathSystem : IEcsRunSystem
{
    private EcsFilter<DeathRequest, ProjectileFragmentTag> _dyingFragmentation;
    private EcsFilter<DeathRequest, ProjectileComponent> _dyingProjectiles;

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

public class ProjectilesVampirismFinalDeathSystem : IEcsRunSystem
{
    private EcsFilter<DeathRequest, ProjectileComponent, ElementalParticlesComponent> _dyingVampirismProjectiles;

    public void Run()
    {
        foreach (var i in _dyingVampirismProjectiles)
        {
            ref var particles = ref _dyingVampirismProjectiles.Get3(i);
            particles.VampirismFx.emitting = false;
        }
    }
}


