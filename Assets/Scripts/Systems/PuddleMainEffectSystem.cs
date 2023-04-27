using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine;

public class PuddleMainEffectSystem : IEcsRunSystem
{
    private EcsFilter<PuddleData, TransformComponent, PuddleAffectedTargetsComponent, PuddleTag> _puddlesFilter;
    private EcsFilter<TransformComponent, SpeedMultiplierComponent, EnemyTag> _enemiesFilter;

    private WeaponUpgradeLevels _weaponUpgrades;

    public void Run()
    {
        foreach (var i in _puddlesFilter)
        {
            ref var puddleEntity = ref _puddlesFilter.GetEntity(i);
            ref var puddleData = ref _puddlesFilter.Get1(i);
            ref var puddleTransform = ref _puddlesFilter.Get2(i);
            ref var puddleAffectedTargets = ref _puddlesFilter.Get3(i);

            puddleAffectedTargets.AffectedTargets.Clear();

            foreach (var j in _enemiesFilter)
            {
                ref var enemyEntity = ref _enemiesFilter.GetEntity(j);
                ref var enemyTransform = ref _enemiesFilter.Get1(j);
                ref var enemySpeedMultiplier = ref _enemiesFilter.Get2(j);

                if (Vector2.Distance(enemyTransform.Transform.position, puddleTransform.Transform.position) <= puddleData.Radius)
                {
                    puddleAffectedTargets.AffectedTargets.Add(enemyEntity);
                    enemySpeedMultiplier.Multiplier *= puddleData.Efficiency;
                }
            }
        }
    }
}

