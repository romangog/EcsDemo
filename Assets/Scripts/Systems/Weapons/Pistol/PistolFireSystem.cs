using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leopotam.Ecs;

public class PistolFireSystem : IEcsRunSystem
{
    private EcsWorld _world = null;

    EcsFilter<PistolWeaponComponent, PistolFiringStatusComponent> _pistolFiringFilter;
    EcsFilter<EnemyTag, RigidbodyComponent> _enemiesRigidbodies;
    EcsFilter<PlayerTag, RigidbodyComponent> _playerRigidbody;

    private readonly GameObject _pistolShotPrefab;
    private Prefabs _prefabs = null;


    public void Run()
    {
        Vector2 playerPos = _playerRigidbody.Get2(0).Rigidbody.position;

        Vector2 closestPos = Vector2.zero;
        float minDistance = float.MaxValue;
        bool areEnemiesAround = false;
        foreach (var enemyId in _enemiesRigidbodies)
        {
            areEnemiesAround = true;
            ref var rigidbody = ref _enemiesRigidbodies.Get2(enemyId);
            float distance = Vector2.Distance(rigidbody.Rigidbody.position, playerPos);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestPos = rigidbody.Rigidbody.position;
            }
        }

        foreach (var pistolId in _pistolFiringFilter)
        {
            ref var pistolWeapon = ref _pistolFiringFilter.Get1(pistolId);

            ref var pistolFiringStatus = ref _pistolFiringFilter.Get2(pistolId);

            pistolFiringStatus.CurrentReload -= Time.deltaTime;
            pistolFiringStatus.CurrentReload = Mathf.Max(0f, pistolFiringStatus.CurrentReload);

            if (areEnemiesAround
                && pistolFiringStatus.CurrentReload == 0f)
            {
                pistolFiringStatus.CurrentReload = 1f / pistolWeapon.ShootingFrequency;

                var entity = _world.NewEntity();

                Projectile pistolShot = GameObject.Instantiate(_prefabs.PistolShotPrefab);
                pistolShot.transform.position = playerPos;
                pistolShot.transform.rotation = Quaternion.LookRotation(Vector3.forward, closestPos - playerPos);
                pistolShot.ProjectileHitbox.HitEnityRecievedEvent += (hitEntityRef)=> OnHitEntityRecieved(hitEntityRef,ref entity);

                entity.Get<RigidbodyComponent>().Rigidbody = pistolShot.Rigidbody;
                entity.Get<MoveForwardComponent>().Direction = (closestPos - playerPos).normalized;
                entity.Get<MoveForwardComponent>().Speed = 10f;
                entity.Get<PistolProjectileHitComponent>().Damage = pistolWeapon.Damage;
            }
        }
    }

    private void OnHitEntityRecieved(EntityReference hitEntityRef,ref EcsEntity pistolShotEntity)
    {
        Debug.Log("Hit enemy " + hitEntityRef.name);
        hitEntityRef.Entity.Get<AccumulativeDamageComponent>().Damage += pistolShotEntity.Get<PistolProjectileHitComponent>().Damage;
        pistolShotEntity.Get<PistolShotDestroyTag>();
    }
}
