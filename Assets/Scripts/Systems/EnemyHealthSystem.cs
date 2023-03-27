using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leopotam.Ecs;

public class EnemyHealthSystem : IEcsRunSystem
{
    EcsFilter<AccumulativeDamageComponent, HealthComponent, EnemyTag> _enemyDamageFilter;

    public void Run()
    {
        foreach (var i in _enemyDamageFilter)
        {
            Debug.Log("Decreasy enemy healtrh ");
            ref var accumDamage = ref _enemyDamageFilter.Get1(i);
            ref var health = ref _enemyDamageFilter.Get2(i);
            ref var entity = ref _enemyDamageFilter.GetEntity(i);

            health.CurrentHealth -= accumDamage.Damage;
            entity.Del<AccumulativeDamageComponent>();
        }
    }
}
