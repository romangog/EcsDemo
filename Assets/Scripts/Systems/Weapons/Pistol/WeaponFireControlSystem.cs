using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leopotam.Ecs;

public class WeaponFireControlSystem : IEcsRunSystem
{
    private EcsWorld _world = null;

    EcsFilter<WeaponFiringStatusComponent, PistolTag> _pistolFiringFilter;
    EcsFilter<EnemyTag, RigidbodyComponent>.Exclude<DeadTag> _enemiesRigidbodies;
    EcsFilter<PlayerTag, RigidbodyComponent>.Exclude<DeadTag> _playerRigidbodyFilter;

    private WeaponUpgradeLevels _upgradeLevels = null;

    public void Run()
    {
        if (_playerRigidbodyFilter.GetEntitiesCount() == 0) return;
        Vector2 playerPos = _playerRigidbodyFilter.Get2(0).Rigidbody.position;
        ref var playerEntity =ref _playerRigidbodyFilter.GetEntity(0);
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
            ref var pistolFiringStatus = ref _pistolFiringFilter.Get1(pistolId);

            pistolFiringStatus.CurrentReload -= Time.deltaTime;
            pistolFiringStatus.CurrentReload = Mathf.Max(0f, pistolFiringStatus.CurrentReload);

            if (areEnemiesAround
                && pistolFiringStatus.CurrentReload == 0f)
            {
                var spawnBulletRequestEntity = _world.NewEntity();

                pistolFiringStatus.CurrentReload = _upgradeLevels.GetShootFrequencyFromLevel();
                ref var spawnBulletsRequest = ref spawnBulletRequestEntity.Get<SpawnBulletsRequest>();
                spawnBulletsRequest.BulletSpawnPos = playerPos;
                spawnBulletsRequest.BulletSpawnDirection = (closestPos - playerPos).normalized;
                spawnBulletsRequest.ShooterEntity = playerEntity;

            }
        }
    }


}

