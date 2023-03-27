using Leopotam.Ecs;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHitbox : MonoBehaviour
{
    public Action<EntityReference> HitEnityRecievedEvent;

    public void PassHitEntity(EntityReference entityRef)
    {
        HitEnityRecievedEvent?.Invoke(entityRef);
    }
}
