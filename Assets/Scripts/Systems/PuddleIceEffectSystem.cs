using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine;

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

public class PuddleLightningEffectSystem : IEcsRunSystem
{
    private EcsFilter<PuddleAffectedTargetsComponent, PuddleLightningEffectTimer, PuddleTag> _puddlesFilter;

    private WeaponUpgradeLevels _weaponUpgrades;
    private EcsWorld _world;
    public void Run()
    {
        if (_weaponUpgrades.LightningLevel == 0) return;

        foreach (var i in _puddlesFilter)
        {
            ref var affectedTargets = ref _puddlesFilter.Get1(i);
            ref var timer = ref _puddlesFilter.Get2(i);
            timer.Timer.Update();
            if (timer.Timer.IsOver)
            {
                timer.Timer.Set(1f);
                List<TransformComponent> targetsTransforms = new List<TransformComponent>();
                foreach (var affectedTarget in affectedTargets.AffectedTargets)
                {
                    affectedTarget.Get<GetHitByLightningRequest>();
                    targetsTransforms.Add(affectedTarget.Get<TransformComponent>());
                }

                if (affectedTargets.AffectedTargets.Count < 2) continue;
                var entity = _world.NewEntity();
                entity.Get<LightningSpawnRequest>().Targets = targetsTransforms;
            }
        }
    }
}


