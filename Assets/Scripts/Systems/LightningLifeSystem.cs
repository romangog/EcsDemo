using Leopotam.Ecs;
using UnityEngine;

public class LightningLifeSystem : IEcsRunSystem
{
    private EcsFilter<TimerComponent, GameObjectComponent, LightningFxTag> _lightningsFilter;

    public void Run()
    {
        foreach (var i in _lightningsFilter)
        {
            ref var lightningEntity = ref _lightningsFilter.GetEntity(i);
            ref var timer = ref _lightningsFilter.Get1(i);
            ref var gameObject = ref _lightningsFilter.Get2(i);

            timer.Timer.Update();

            if(timer.Timer.IsOver)
            {
                GameObject.Destroy(gameObject.GameObject);
                lightningEntity.Destroy();
            }
        }
    }
}




