using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildEntitiesDestroySystem : IEcsRunSystem
{
    private EcsFilter<EntityReferenceComponent> _rootEntities;

    public void Run()
    {
        foreach (var i in _rootEntities)
        {
            ref var entity = ref _rootEntities.GetEntity(i);
            ref var rootEntity = ref _rootEntities.Get1(i);

            if (!rootEntity.RootEntity.IsAlive())
            {
                entity.Destroy();
            }
        }
    }
}
