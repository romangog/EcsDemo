using Leopotam.Ecs;

public class ProjectileSetOnIceSystem : IEcsRunSystem
{
    private EcsFilter<SetBaseColorRequest, ProjectileShotRequest> _projectileShotFilter;

    private WeaponUpgradeLevels _weaponUpgrade = null;
    private GameSettings _gameSettings = null;

    public void Run()
    {
        if (_weaponUpgrade.IceLevel == 0) return;
        foreach (var i in _projectileShotFilter)
        {
            ref var setBaseColor = ref _projectileShotFilter.Get1(i);

            setBaseColor.BaseColor = _gameSettings.IceColor;
        }
    }
}


