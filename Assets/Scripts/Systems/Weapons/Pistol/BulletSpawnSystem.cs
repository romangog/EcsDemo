using UnityEngine;
using Leopotam.Ecs;

public class BulletSpawnSystem : IEcsRunSystem
{
    public EcsFilter<SpawnBulletsRequest> _spawnBulletsFilter;
    private WeaponUpgradeLevels _weaponUpgrade = null;
    private BulletSpawner _bulletSpawner = null;

    public void Run()
    {
        foreach (var i in _spawnBulletsFilter)
        {
            ref var bulletSpawnRequest = ref _spawnBulletsFilter.Get1(i);
            int count = (bulletSpawnRequest.IsFragmentation)? _weaponUpgrade.GetProjectileFragmentationFromLevel() : _weaponUpgrade.GetProjectileMultiplierFromLevel();
            for (int j = 0; j < count; j++)
            {
                var bulletEntity = _bulletSpawner.SpawnBullet(bulletSpawnRequest.BulletSpawnPos, bulletSpawnRequest.BulletSpawnDirection, bulletSpawnRequest.IgnoreEntity);

                if (bulletSpawnRequest.IsFragmentation)
                {
                    bulletEntity.Get<ProjectileFragmentTag>();
                }
            }
        }
    }
}
