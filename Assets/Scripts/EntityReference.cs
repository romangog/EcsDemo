using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leopotam.Ecs;
using System;

public class EntityReference : MonoBehaviour
{
    public EcsEntity Entity;

    public EntityReference[] ChildReferences;

    internal void SetEntity(EcsEntity entity)
    {
        Entity = entity;
        // Without Recursion
        foreach (var childRef in ChildReferences)
        {
            childRef.Entity = entity;
        }

    }
}
