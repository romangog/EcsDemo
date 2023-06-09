﻿using Leopotam.Ecs;
using UnityEngine;
using Voody.UniLeo;

public class BulletSpawner
{
    private readonly EcsWorld _world;
    private readonly Prefabs _prefabs;
    private readonly WeaponUpgradeLevels _weaponUpgradeLevels;
    private readonly GameSettings _gameSettings;
    private readonly IObjectPool<Projectile> _projectilesPool;
    public BulletSpawner(
        EcsWorld ecsWorld, 
        Prefabs prefabs,
        WeaponUpgradeLevels weaponUpgradeLevels,
        GameSettings gameSettings,
        IObjectPool<Projectile> projectilesPool)
    {
        _world = ecsWorld;
        _prefabs = prefabs;
        _weaponUpgradeLevels = weaponUpgradeLevels;
        _gameSettings = gameSettings;
        _projectilesPool = projectilesPool;
    }

    public EcsEntity SpawnBullet(Vector2 pos, Vector2 dir, EcsEntity ignoreEntity, EcsEntity shooter)
    {
        var entity = _world.NewEntity();

        Projectile projectile = _projectilesPool.Get(); // GameObject.Instantiate(_prefabs.ProjectilePrefab);
        projectile.ProjectileHitbox.HitEnityRecievedEvent += (hitEntityRef) => OnHitEntityRecieved(hitEntityRef, ref entity);

        entity.Get<RigidbodyComponent>().Rigidbody = projectile.Rigidbody;
        entity.Get<GameObjectComponent>().GameObject = projectile.gameObject;
        entity.Get<TransformComponent>().Transform = projectile.transform;
        entity.Get<MoveForwardComponent>().Direction = dir;
        entity.Get<IgnoreEnemyComponent>().IgnoreEntity= ignoreEntity;

        projectile.transform.position = pos;

        entity.Get<ProjectileComponent>().Projectile = projectile;
        entity.Get<PathLengthAccumulativeComponent>();
        entity.Get<ProjectileShotRequest>();
        entity.Get<IgnoreEnemyComponent>();
        entity.Get<ProjectileVampirismComponent>();
        entity.Get<ShooterComponent>().ShooterEntity = shooter;
        entity.Get<ProjectilePenetrationComponent>();
        entity.Get<ElementalParticlesComponent>() = projectile.ElementalParticles;
        entity.Get<SpriteRendererComponent>() = projectile.SpriteRenderer;
        entity.Get<SetBaseColorRequest>().BaseColor = _gameSettings.BlackColor;

        return entity;
    }

    private void OnHitEntityRecieved(EntityReference hitEntityRef, ref EcsEntity pistolShotEntity)
    {
        ref var hitRegister = ref pistolShotEntity.Get<HitRegisterRequest>();
        hitRegister.HitTarget = hitEntityRef.Entity;
    }
}
