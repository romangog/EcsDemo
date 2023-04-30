using Leopotam.Ecs;
using UnityEngine;

public class PuddleSpawnElementalEffects : IEcsRunSystem
{
    private EcsFilter<ElementalParticlesComponent, SetBaseColorRequest, PuddleTag, OnSpawnRequest> _spawnedPuddlesFilter;

    private WeaponUpgradeLevels _weaponUpgrades;
    private GameSettings _gameSettings;

    public void Run()
    {
        if (_weaponUpgrades.FireLevel > 0)
            foreach (var i in _spawnedPuddlesFilter)
            {
                ref var entity = ref _spawnedPuddlesFilter.GetEntity(i);
                ref var elementalParticles = ref _spawnedPuddlesFilter.Get1(i);

                entity.Get<PuddleFireEffectTimer>();
                elementalParticles.FireFx.Play();
            }

        if (_weaponUpgrades.IceLevel > 0)
            foreach (var i in _spawnedPuddlesFilter)
            {
                ref var setBaseColor = ref _spawnedPuddlesFilter.Get2(i);
                setBaseColor.BaseColor = _gameSettings.IceColor;
            }

        if (_weaponUpgrades.LightningLevel > 0)
            foreach (var i in _spawnedPuddlesFilter)
            {
                ref var entity = ref _spawnedPuddlesFilter.GetEntity(i);
                ref var elementalParticles = ref _spawnedPuddlesFilter.Get1(i);
                entity.Get<PuddleLightningEffectTimer>();
                elementalParticles.LightningFx.Play();
            }
    }
}


