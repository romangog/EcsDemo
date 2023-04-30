using Leopotam.Ecs;
using UnityEngine;

public class PuddleFireEffectSystem : IEcsRunSystem
{
    private EcsFilter<PuddleAffectedTargetsComponent, PuddleFireEffectTimer, PuddleTag> _puddlesFilter;

    private WeaponUpgradeLevels _weaponUpgrades;

    public void Run()
    {
        if (_weaponUpgrades.FireLevel == 0) return;
        foreach (var i in _puddlesFilter)
        {
            ref var affectedTargets = ref _puddlesFilter.Get1(i);
            ref var timer = ref _puddlesFilter.Get2(i);

            timer.Timer.Update();
            if (timer.Timer.IsOver)
            {
                timer.Timer.Set(1f);
                foreach (var affectedTarget in affectedTargets.AffectedTargets)
                {
                    affectedTarget.Get<CatchFireRequest>();
                }
            }
        }
    }
}

