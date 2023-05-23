using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leopotam.Ecs;
using Voody.UniLeo;

public class PlayerHealthSystem : IEcsRunSystem
{
    private EcsFilter<AccumulativeDamageComponent, HealthComponent, PlayerTag>.Exclude<PlayerInvulComponent, DeadTag> _playerDamageHealthFilter;
    private EcsFilter<AccumulativeHealComponent, HealthComponent, PlayerTag>.Exclude<DeadTag> _playerHealFilter;

    public void Run()
    {
        foreach (var id in _playerDamageHealthFilter)
        {
            ref var entity = ref _playerDamageHealthFilter.GetEntity(id);
            ref var damage = ref _playerDamageHealthFilter.Get1(id);
            ref var health = ref _playerDamageHealthFilter.Get2(id);
            health.CurrentHealth = Mathf.Max(0f, health.CurrentHealth - damage.Damage);
            entity.Del<AccumulativeDamageComponent>();

            if (health.CurrentHealth == 0f)
            {
                entity.Get<DeathRequest>();
            }
            else
            {
                entity.Get<PlayerInvulComponent>().TimeLeft = 2f;
            }
        }

        foreach (var i in _playerHealFilter)
        {
            ref var entity = ref _playerHealFilter.GetEntity(i);
            ref var heal = ref _playerHealFilter.Get1(i);
            ref var health = ref _playerHealFilter.Get2(i);
            health.CurrentHealth = Mathf.MoveTowards(health.CurrentHealth, health.MaxHealth, heal.Heal);
            entity.Del<AccumulativeHealComponent>();
        }
    }

}