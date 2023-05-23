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
            ref var bulletSpawnRequestEntity = ref _spawnBulletsFilter.GetEntity(i);
            int count = (bulletSpawnRequest.IsFragmentation) ? _weaponUpgrade.GetProjectileFragmentationFromLevel() : _weaponUpgrade.GetProjectileMultiplierFromLevel();
            for (int j = 0; j < count; j++)
            {
                var bulletEntity = _bulletSpawner.SpawnBullet(
                    bulletSpawnRequest.BulletSpawnPos,
                    bulletSpawnRequest.BulletSpawnDirection,
                    bulletSpawnRequest.IgnoreEntity,
                    bulletSpawnRequest.ShooterEntity);

                if (bulletSpawnRequest.IsFragmentation)
                {
                    bulletEntity.Get<ProjectileFragmentTag>();

                    ref var vampirism = ref bulletSpawnRequestEntity.Get<ProjectileVampirismComponent>();
                    if (!vampirism.VampirismTimer.IsOver)
                    {
                        bulletEntity.Get<ProjectileVampirismComponent>().VampirismTimer.Set(vampirism.VampirismTimer.TimeLeft);
                    }

                }

            }
        }
    }
}
