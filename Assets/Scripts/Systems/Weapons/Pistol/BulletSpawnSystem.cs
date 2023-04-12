using UnityEngine;
using Leopotam.Ecs;

public class BulletSpawnSystem : IEcsRunSystem
{
    public EcsFilter<SpawnBulletsRequest> _spawnBuleltsFilter;

    private EcsWorld _world = null;
    private Prefabs _prefabs = null;
    private WeaponUpgradeLevels _weaponUpgrade = null;

    public void Run()
    {
        foreach (var i in _spawnBuleltsFilter)
        {
            ref var bulletSpawnRequest = ref _spawnBuleltsFilter.Get1(i);

            var entity = _world.NewEntity();

            Projectile pistolShot = GameObject.Instantiate(_prefabs.PistolShotPrefab);
            pistolShot.ProjectileHitbox.HitEnityRecievedEvent += (hitEntityRef) => OnHitEntityRecieved(hitEntityRef, ref entity);

            entity.Get<RigidbodyComponent>().Rigidbody = pistolShot.Rigidbody;
            entity.Get<GameObjectComponent>().GameObject = pistolShot.gameObject;
            entity.Get<TransformComponent>().Transform= pistolShot.transform;
            entity.Get<MoveForwardComponent>().Direction = bulletSpawnRequest.DirectionToClosestTarget;

            pistolShot.transform.position = bulletSpawnRequest.BulletSpawnPos;


            // Adjust SizeComponent - TURN INTO SYSTEM
            ref var projectileSize = ref entity.Get<ProjectileSizeComponent>();
            projectileSize.Size = _weaponUpgrade.GetProjectileSizeFromLevel();
            pistolShot.transform.localScale = Vector3.one * projectileSize.Size;

            // Adjust PenetrationComponent - TURN INTO SYSTEM
            ref var projectilePenetration = ref entity.Get<ProjectilePenetrationComponent>();
            projectilePenetration.PenetrationsLeft = _weaponUpgrade.PenetrationLevel;

            entity.Get<ProjectileTag>();
            entity.Get<PathLengthAccumulativeComponent>();
            entity.Get<ProjectileShotRequest>();
        }
    }

    private void OnHitEntityRecieved(EntityReference hitEntityRef, ref EcsEntity pistolShotEntity)
    {
        hitEntityRef.Entity.Get<AccumulativeDamageComponent>().Damage += pistolShotEntity.Get<ProjectileDamageComponent>().Damage;
        hitEntityRef.Entity.Get<HitImpactRequest>().PushForce +=
            pistolShotEntity.Get<MoveForwardComponent>().Direction * _weaponUpgrade.GetThrowbackForceFromLevel();

        ref var penetration = ref pistolShotEntity.Get<ProjectilePenetrationComponent>();
        if (penetration.PenetrationsLeft == 0)
        {
            pistolShotEntity.Get<DeathRequest>();
        }
        penetration.PenetrationsLeft--;
    }
}
