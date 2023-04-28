using Leopotam.Ecs;
using UnityEngine;

public class PuddleFireEffectSystem : IEcsRunSystem
{
    private EcsFilter<PuddleAffectedTargetsComponent, PuddleTag> _puddlesFilter;

    private WeaponUpgradeLevels _weaponUpgrades;

    public void Run()
    {
        if (_weaponUpgrades.FireLevel == 0) return;
        foreach (var i in _puddlesFilter)
        {
            ref var affectedTargets = ref _puddlesFilter.Get1(i);

            foreach (var affectedTarget in affectedTargets.AffectedTargets)
            {
                affectedTarget.Get<CatchFireRequest>();
            }
        }
    }
}


public class PuddleIceEffectSystem : IEcsRunSystem
{
    private EcsFilter<PuddleAffectedTargetsComponent, PuddleTag> _puddlesFilter;

    private WeaponUpgradeLevels _weaponUpgrades;

    public void Run()
    {
        if (_weaponUpgrades.IceLevel == 0) return;
        foreach (var i in _puddlesFilter)
        {
            ref var affectedTargets = ref _puddlesFilter.Get1(i);

            foreach (var affectedTarget in affectedTargets.AffectedTargets)
            {
                affectedTarget.Get<CatchIceRequest>();
            }
        }
    }
}

