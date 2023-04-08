using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leopotam.Ecs;

public struct EntityReferenceComponent
{
    public EcsEntity RootEntity;
}

public class InitializeEntityReferencesSystem : IEcsRunSystem
{
    private EcsFilter<EntityRootRequest> _entityinitializeRequestFilter;
    private EcsFilter<EntityChildRequest> _entityChildRequest;

    public void Run()
    {
        foreach (var id in _entityinitializeRequestFilter)
        {
            ref var entity = ref _entityinitializeRequestFilter.GetEntity(id);
            ref var root = ref _entityinitializeRequestFilter.Get1(id);

            root.EntityReference.Entity = entity;
            entity.Del<EntityRootRequest>();
        }

        foreach (var id in _entityChildRequest)
        {
            ref var entity = ref _entityChildRequest.GetEntity(id);
            ref var child = ref _entityChildRequest.Get1(id);

            entity.Get<EntityReferenceComponent>().RootEntity = child.EntityReference.Entity;
            entity.Del<EntityChildRequest>();
        }
    }
}
