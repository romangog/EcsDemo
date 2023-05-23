using Leopotam.Ecs;
using UnityEngine;

public class FragmentationSpawnSystem : IEcsRunSystem
{
    private EcsFilter<HitRegisterRequest, TransformComponent, MoveForwardComponent, ShooterComponent, ProjectileComponent, DeathRequest>
        .Exclude<ProjectileFragmentTag> _dyingProjectiles;

    private WeaponUpgradeLevels _weaponUpgrades;
    private EcsWorld _world;
    public void Run()
    {
        if (_weaponUpgrades.FragmentationLevel == 0) return;

        foreach (var i in _dyingProjectiles)
        {
            ref var projectileEntity = ref _dyingProjectiles.GetEntity(i);
            ref var hitRegister = ref _dyingProjectiles.Get1(i);
            ref var projectileTransform = ref _dyingProjectiles.Get2(i);
            ref var projectileMoveForward = ref _dyingProjectiles.Get3(i);
            ref var shooter = ref _dyingProjectiles.Get4(i);

            int fragmentation = _weaponUpgrades.GetProjectileFragmentationFromLevel();

            if (fragmentation > 0)
            {
                var spawnBulletsEntity = _world.NewEntity();
                ref var spawnBulletsRequest = ref spawnBulletsEntity.Get<SpawnBulletsRequest>();
                spawnBulletsRequest.BulletSpawnPos = projectileTransform.Transform.position;
                spawnBulletsRequest.BulletSpawnDirection = projectileMoveForward.Direction;
                spawnBulletsRequest.IsFragmentation = true;
                spawnBulletsRequest.IgnoreEntity = hitRegister.HitTarget;
                spawnBulletsRequest.ShooterEntity = shooter.ShooterEntity;

                if (projectileEntity.Has<ProjectileVampirismComponent>())
                {
                    ref var vampirism = ref projectileEntity.Get<ProjectileVampirismComponent>();
                    spawnBulletsEntity.Get<ProjectileVampirismComponent>().VampirismTimer.Set(vampirism.VampirismTimer.TimeLeft);
                }
            }
        }
    }
}


