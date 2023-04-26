using Leopotam.Ecs;
using UnityEngine;

public class PuddleLifeEndSystem : IEcsRunSystem
{
    private EcsFilter<PuddleData,GameObjectComponent, PuddleTag> _puddlesFilter;

    public void Run()
    {
        foreach (var i in _puddlesFilter)
        {
            ref var entity = ref _puddlesFilter.GetEntity(i);
            ref var puddleData = ref _puddlesFilter.Get1(i);
            ref var puddleGO = ref _puddlesFilter.Get2(i);

            puddleData.LifeTimer.Update();
            if(puddleData.LifeTimer.IsOver)
            {
                GameObject.Destroy(puddleGO.GameObject);
                entity.Destroy();
            }
        }
    }
}


