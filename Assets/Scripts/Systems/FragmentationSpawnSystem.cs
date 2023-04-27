using Leopotam.Ecs;

public class FragmentationSpawnSystem : IEcsRunSystem
{
    private EcsFilter<HitRegisterRequest, TransformComponent, MoveForwardComponent, ProjectileTag, DeathRequest>
        .Exclude<ProjectileFragmentTag> _dyingProjectiles;

    private WeaponUpgradeLevels _weaponUpgrades;
    private EcsWorld _world;
    public void Run()
    {
        if (_weaponUpgrades.FragmentationLevel == 0) return;

        foreach (var i in _dyingProjectiles)
        {
            ref var hitRegister = ref _dyingProjectiles.Get1(i);
            ref var projectileTransform = ref _dyingProjectiles.Get2(i);
            ref var projectileMoveForward = ref _dyingProjectiles.Get3(i);

            int fragmentation = _weaponUpgrades.GetProjectileFragmentationFromLevel();

            if (fragmentation > 0)
            {
                var entity = _world.NewEntity();
                ref var spawnBulletsRequest = ref entity.Get<SpawnBulletsRequest>();
                spawnBulletsRequest.BulletSpawnPos = projectileTransform.Transform.position;
                spawnBulletsRequest.BulletSpawnDirection = projectileMoveForward.Direction;
                spawnBulletsRequest.IsFragmentation = true;
                spawnBulletsRequest.IgnoreEntity = hitRegister.HitTarget;
            }
        }
    }
}


