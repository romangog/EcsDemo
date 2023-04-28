﻿using Leopotam.Ecs;
using UnityEngine;

public class PuddleSpawnFireEffectSystem : IEcsRunSystem
{
    private EcsFilter<PuddleTag, OnSpawnRequest, PuddleParticlesComponent> _spawnedPuddlesFilter;

    private WeaponUpgradeLevels _weaponUpgrades;

    public void Run()
    {
        if (_weaponUpgrades.FireLevel == 0) return;

        foreach (var i in _spawnedPuddlesFilter)
        {
            ref var puddleEntity = ref _spawnedPuddlesFilter.GetEntity(i);
            ref var puddleParticles = ref _spawnedPuddlesFilter.Get3(i);

            puddleParticles.FireFx.Play();
        }
    }
}

public class PuddleSpawnIceEffectSystem : IEcsRunSystem
{
    private EcsFilter<SetBaseColorRequest, PuddleTag, OnSpawnRequest> _spawnedPuddlesFilter;

    private WeaponUpgradeLevels _weaponUpgrades;
    private GameSettings _gameSettings;

    public void Run()
    {
        if (_weaponUpgrades.IceLevel == 0) return;

        foreach (var i in _spawnedPuddlesFilter)
        {
            ref var puddleEntity = ref _spawnedPuddlesFilter.GetEntity(i);
            ref var setBaseColor = ref _spawnedPuddlesFilter.Get1(i);
            setBaseColor.BaseColor = _gameSettings.IceColor;
        }
    }
}




