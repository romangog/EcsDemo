using Leopotam.Ecs;

public class PuddleSpawnIceEffectSystem : IEcsRunSystem
{
    private EcsFilter<SetBaseColorRequest, PuddleTag, OnSpawnRequest> _spawnedPuddlesFilter;

    private WeaponUpgradeLevels _weaponUpgrades;
    private GameSettings _gameSettings;

    public void Run()
    {

    }
}


