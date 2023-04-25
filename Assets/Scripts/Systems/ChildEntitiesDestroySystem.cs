using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildEntitiesDestroySystem : IEcsRunSystem
{
    private EcsFilter<EntityReferenceComponent> _childEntities;

    public void Run()
    {
        foreach (var i in _childEntities)
        {
            ref var entity = ref _childEntities.GetEntity(i);
            ref var rootEntity = ref _childEntities.Get1(i);

            if (!rootEntity.RootEntity.IsAlive())
            {
                entity.Destroy();
            }
        }
    }
}
