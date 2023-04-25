using Leopotam.Ecs;
using System;

[Serializable]
public struct AccumulativeDamageComponent
{
    public float Damage;
}

public struct ProjectileHitRegistration
{
    public EcsEntity HitEntity;
}
