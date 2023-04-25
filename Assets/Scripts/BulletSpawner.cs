using Leopotam.Ecs;
using UnityEngine;
using Voody.UniLeo;

public class BulletSpawner
{
    private readonly EcsWorld _world;
    private readonly Prefabs _prefabs;
    private readonly WeaponUpgradeLevels _weaponUpgradeLevels;
    public BulletSpawner(
        EcsWorld ecsWorld, 
        Prefabs prefabs,
        WeaponUpgradeLevels weaponUpgradeLevels)
    {
        _world = ecsWorld;
        _prefabs = prefabs;
        _weaponUpgradeLevels = weaponUpgradeLevels;
    }

    public EcsEntity SpawnBullet(Vector2 pos, Vector2 dir, EcsEntity ignoreEntity)
    {
        var entity = _world.NewEntity();

        Projectile pistolShot = GameObject.Instantiate(_prefabs.PistolShotPrefab);
        pistolShot.ProjectileHitbox.HitEnityRecievedEvent += (hitEntityRef) => OnHitEntityRecieved(hitEntityRef, ref entity);

        entity.Get<RigidbodyComponent>().Rigidbody = pistolShot.Rigidbody;
        entity.Get<GameObjectComponent>().GameObject = pistolShot.gameObject;
        entity.Get<TransformComponent>().Transform = pistolShot.transform;
        entity.Get<MoveForwardComponent>().Direction = dir;
        entity.Get<IgnoreEnemyComponent>().IgnoreEntity= ignoreEntity;

        pistolShot.transform.position = pos;

        entity.Get<ProjectileTag>();
        entity.Get<PathLengthAccumulativeComponent>();
        entity.Get<ProjectileShotRequest>();
        entity.Get<IgnoreEnemyComponent>();
        entity.Get<ProjectilePenetrationComponent>();
        return entity;
    }

    private void OnHitEntityRecieved(EntityReference hitEntityRef, ref EcsEntity pistolShotEntity)
    {
        pistolShotEntity.Get<HitRegisterRequest>().HitTarget = hitEntityRef.Entity;
    }
}
