using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeadDisableSystem : IEcsRunSystem
{
    private EcsFilter<EnemyTag, DeadTag, TimerComponent> _deadEnemiesTimer;

    public void Run()
    {
        foreach (var i in _deadEnemiesTimer)
        {
            ref var entity = ref _deadEnemiesTimer.GetEntity(i);
            ref var timer = ref _deadEnemiesTimer.Get3(i);

            timer.Timer.Update();

            if(timer.Timer.IsOver)
            {
                GameObject.Destroy(entity.Get<GameObjectComponent>().GameObject, 0.1f);
                entity.Destroy();
            }
        }    
    }
}

