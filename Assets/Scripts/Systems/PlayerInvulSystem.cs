using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leopotam.Ecs;

public class PlayerInvulSystem : IEcsRunSystem
{
    private EcsFilter<PlayerTag, PlayerInvulComponent> _playerInvulSystem;
    public void Run()
    {
        foreach (var i in _playerInvulSystem)
        {
            ref var invul = ref _playerInvulSystem.Get2(i);
            ref var entity = ref _playerInvulSystem.GetEntity(i);
            invul.TimeLeft = Mathf.MoveTowards(invul.TimeLeft,0f, Time.deltaTime);
            if(invul.TimeLeft == 0f)
            {
                entity.Del<PlayerInvulComponent>();
            }
            else
            {
                entity.Del<AccumulativeDamageComponent>();
            }
        }
    }
}
