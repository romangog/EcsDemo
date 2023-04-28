using Leopotam.Ecs;

public class EnemyCatchFireSystem : IEcsRunSystem
{
    private EcsFilter<EnemyTag, CatchFireRequest, EnemyParticlesComponent>.Exclude<TargetOnFireComponent> _enemiesFilter;

    private WeaponUpgradeLevels _weaponUpgrade;

    public void Run()
    {
        foreach (var i in _enemiesFilter)
        {
            ref var targetEntity = ref _enemiesFilter.GetEntity(i);

            ref var onFire = ref targetEntity.Get<TargetOnFireComponent>();
            onFire.DamagePerSec = _weaponUpgrade.GetFireDamagePerSecFromLevel();
            onFire.FireCatchRadius = _weaponUpgrade.GetFireCatchRadiusFromLevel();
            onFire.FireTimer.Set(_weaponUpgrade.GetFireTimerFromLevel());
            onFire.FireDamageTickTimer.Set(1f);
            targetEntity.Get<EnemyParticlesComponent>().OnFireFx.Play();
        }
    }
}





