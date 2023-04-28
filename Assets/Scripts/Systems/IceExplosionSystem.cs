using Leopotam.Ecs;

public class IceExplosionSystem : IEcsRunSystem
{
    private EcsFilter<AffectedTargets, SetBaseColorRequest, ExplosionTag, OnSpawnRequest> _explosionsFilter;

    private WeaponUpgradeLevels _weaponUpgrade;
    private GameSettings _gameSettings;

    public void Run()
    {
        if (_weaponUpgrade.IceLevel == 0) return;

        foreach (var i in _explosionsFilter)
        {
            ref var explosionAffectedTargets = ref _explosionsFilter.Get1(i);
            ref var setBaseColor = ref _explosionsFilter.Get2(i);

            setBaseColor.BaseColor = _gameSettings.IceColor;

            foreach (var target in explosionAffectedTargets.Targets)
            {
                if (!target.IsAlive()) continue;
                target.Get<CatchIceRequest>();
            }
        }
    }
}



