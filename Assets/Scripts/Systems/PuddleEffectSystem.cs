using Leopotam.Ecs;
using UnityEngine;

public class PuddleEffectSystem : IEcsRunSystem
{
    private EcsFilter<PuddleData,TransformComponent, PuddleTag> _puddlesFilter;
    private EcsFilter<TransformComponent, SpeedMultiplierComponent, EnemyTag> _enemiesFilter;

    public void Run()
    {
        foreach (var i in _puddlesFilter)
        {
            ref var entity = ref _puddlesFilter.GetEntity(i);
            ref var puddleData = ref _puddlesFilter.Get1(i);
            ref var puddleTransform = ref _puddlesFilter.Get2(i);

            foreach (var j in _enemiesFilter)
            {
                ref var enemyTransform = ref _enemiesFilter.Get1(j);
                ref var enemySpeedMultiplier = ref _enemiesFilter.Get2(j);

                if (Vector2.Distance(enemyTransform.Transform.position, puddleTransform.Transform.position) <= puddleData.Radius)
                {
                    enemySpeedMultiplier.Multiplier *= puddleData.Efficiency;
                }
            }
        }
    }
}


