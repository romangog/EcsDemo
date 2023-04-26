using Leopotam.Ecs;

public class ProjectileFragmentationLevelSystem : IEcsRunSystem
{
    private EcsFilter<ProjectileHitRequest> _projectileShotFilter;

    private WeaponUpgradeLevels _weaponUpgrade = null;

    public void Run()
    {
        foreach (var i in _projectileShotFilter)
        {
            ref var entity = ref _projectileShotFilter.GetEntity(i);
        }
    }
}

// SPLIT SYSTEMS - FILTER IS TOO EHAVY
public class EnemyHitRegistrationSystem : IEcsRunSystem
{
    private EcsFilter<
        HitRegisterRequest,
        MoveForwardComponent,
        TransformComponent,
        ProjectilePenetrationComponent,
        IgnoreEnemyComponent> _projecitlesFilter;

    private WeaponUpgradeLevels _weaponUpgradeLevels;
    private EcsWorld _world;

    public void Run()
    {
        foreach (var i in _projecitlesFilter)
        {
            ref var pistolShotEntity = ref _projecitlesFilter.GetEntity(i);
            ref var targetEntity = ref _projecitlesFilter.Get1(i).HitTarget;
            ref var bulletMoveForward = ref _projecitlesFilter.Get2(i);
            ref var bulletTransform = ref _projecitlesFilter.Get3(i);
            ref var bulletPenetration = ref _projecitlesFilter.Get4(i);
            ref var ignoreEnemy = ref _projecitlesFilter.Get5(i);

            if (ignoreEnemy.IgnoreEntity == targetEntity)
            {
                return;
            }

            if (!targetEntity.IsAlive()) return;

            targetEntity.Get<AccumulativeDamageComponent>().Damage += pistolShotEntity.Get<ProjectileDamageComponent>().Damage;
            targetEntity.Get<HitImpactRequest>().PushForce +=
                bulletMoveForward.Direction * _weaponUpgradeLevels.GetThrowbackForceFromLevel();


            // Set on fire -- Move into separate system
            if(_weaponUpgradeLevels.FireLevel > 0)
            {
                ref var onFire= ref targetEntity.Get<TargetOnFireComponent>();
                onFire.DamagePerSec = _weaponUpgradeLevels.GetFireDamagePerSecFromLevel();
                onFire.FireCatchRadius = _weaponUpgradeLevels.GetFireCatchRadiusFromLevel();
                onFire.FireTimer.Set(_weaponUpgradeLevels.GetFireTimerFromLevel());
                onFire.FireDamageTickTimer.Set(1f);
                targetEntity.Get<EnemyParticlesComponent>().OnFireFx.Play();
            }

            if (bulletPenetration.PenetrationsLeft == 0)
            {
                pistolShotEntity.Get<DeathRequest>();
            }
            bulletPenetration.PenetrationsLeft--;
        }
    }
}



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

public class FragmentationFinalDeathSystem : IEcsRunSystem
{
    private EcsFilter<DeathRequest, ProjectileFragmentTag> _dyingFragmentation;
    private EcsFilter<DeathRequest, ProjectileTag> _dyingProjectiles;

    private WeaponUpgradeLevels _weaponUpgrades;

    public void Run()
    {
        foreach (var i in _dyingFragmentation)
        {
            ref var projectileEntity = ref _dyingFragmentation.GetEntity(i);
            projectileEntity.Get<ProjectileFinalDeathRequest>();
        }

        if (_weaponUpgrades.FragmentationLevel == 0)
        {
            foreach (var i in _dyingProjectiles)
            {
                ref var projectileEntity = ref _dyingProjectiles.GetEntity(i);
                projectileEntity.Get<ProjectileFinalDeathRequest>();
            }
        }
    }
}


