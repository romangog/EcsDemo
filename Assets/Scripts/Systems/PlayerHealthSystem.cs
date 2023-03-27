using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leopotam.Ecs;
using Voody.UniLeo;

public class PlayerHealthSystem : IEcsRunSystem
{
    private EcsFilter<AccumulativeDamageComponent, HealthComponent, PlayerTag>.Exclude<PlayerInvulComponent> _playerDamageHealthFilter;

    public void Run()
    {
        foreach (var id in _playerDamageHealthFilter)
        {
            ref var entity = ref _playerDamageHealthFilter.GetEntity(id);
            ref var damage = ref _playerDamageHealthFilter.Get1(id);
            ref var health = ref _playerDamageHealthFilter.Get2(id);
            health.CurrentHealth = Mathf.Max(0f, health.CurrentHealth - damage.Damage);
            entity.Del<AccumulativeDamageComponent>();
            entity.Get<PlayerInvulComponent>().TimeLeft = 2f;
        }
    }

}
