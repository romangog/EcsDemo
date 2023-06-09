﻿using Leopotam.Ecs;

public class EnemyCatchIceSystem : IEcsRunSystem
{
    private EcsFilter<EnemyTag, CatchIceRequest>.Exclude<TargetInIceComponent> _enemiesFilter;

    private WeaponUpgradeLevels _weaponUpgrade;
    private GameSettings _gameSettings;

    public void Run()
    {
        foreach (var i in _enemiesFilter)
        {
            ref var targetEntity = ref _enemiesFilter.GetEntity(i);

            ref var inIce = ref targetEntity.Get<TargetInIceComponent>();
            inIce.IceTimer.Set(_weaponUpgrade.GetIceTimerFromLevel());
            inIce.IceDamageMultiplier = _weaponUpgrade.GetIceDamageMultiplierFromLevel();
            targetEntity.Get<SetBaseColorRequest>().BaseColor = _gameSettings.IceColor;
        }
    }
}

public class EnemyGetLightningHittedSystem : IEcsRunSystem
{
    private EcsFilter<ElementalParticlesComponent, EnemyTag, GetHitByLightningRequest>.Exclude<TargetInIceComponent> _enemiesFilter;

    private WeaponUpgradeLevels _weaponUpgrade;
    private GameSettings _gameSettings;

    public void Run()
    {
        foreach (var i in _enemiesFilter)
        {
            ref var targetEntity = ref _enemiesFilter.GetEntity(i);
            ref var elementalParticles = ref _enemiesFilter.Get1(i);
            targetEntity.Get<AccumulativeDamageComponent>().Damage += _weaponUpgrade.GetLightningHitDamageFromLevel();
            elementalParticles.LightningFx.Play();
        }
    }
}





