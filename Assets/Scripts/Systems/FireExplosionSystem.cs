using Leopotam.Ecs;

public class FireExplosionSystem : IEcsRunSystem
{
    private EcsFilter<AffectedTargetsComponent, ElementalParticlesComponent, ExplosionTag, OnSpawnRequest> _explosionsFilter;

    private WeaponUpgradeLevels _weaponUpgrade;
    public void Run()
    {
        if (_weaponUpgrade.FireLevel == 0) return;

        foreach (var i in _explosionsFilter)
        {
            ref var explosionAffectedTargets = ref _explosionsFilter.Get1(i);
            ref var elementalParticles = ref _explosionsFilter.Get2(i);

            elementalParticles.FireFx.Play();

            foreach (var target in explosionAffectedTargets.Targets)
            {
                if (!target.IsAlive()) continue;
                target.Get<CatchFireRequest>();
            }
        }
    }
}



