using Leopotam.Ecs;
using UnityEngine;

public struct SpawnBulletsRequest
{
    public Vector2 BulletSpawnPos;
    public Vector2 BulletSpawnDirection;
    public bool IsFragmentation;
    public EcsEntity IgnoreEntity;
    public EcsEntity ShooterEntity;
    public EcsEntity EmitterProjectile;
}
