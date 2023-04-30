using Leopotam.Ecs;

public class ProjectileSetOnFireSystem : IEcsRunSystem
{
    private EcsFilter<ElementalParticlesComponent, ProjectileShotRequest> _projectileShotFilter;

    private WeaponUpgradeLevels _weaponUpgrade = null;

    public void Run()
    {
        if (_weaponUpgrade.FireLevel == 0) return;
        foreach (var i in _projectileShotFilter)
        {
            ref var elementalParticles = ref _projectileShotFilter.Get1(i);

            elementalParticles.FireFx.Play();
        }
    }
}


