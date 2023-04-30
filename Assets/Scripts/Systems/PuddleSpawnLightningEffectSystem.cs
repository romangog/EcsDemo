using Leopotam.Ecs;

public class PuddleSpawnLightningEffectSystem : IEcsRunSystem
{
    private EcsFilter<ElementalParticlesComponent, PuddleTag, OnSpawnRequest> _spawnedPuddlesFilter;

    private WeaponUpgradeLevels _weaponUpgrades;
    private GameSettings _gameSettings;

    public void Run()
    {
        if (_weaponUpgrades.LightningLevel == 0) return;

        foreach (var i in _spawnedPuddlesFilter)
        {
            ref var elementalParticles = ref _spawnedPuddlesFilter.Get1(i);
            elementalParticles.LightningFx.Play();
        }
    }
}


