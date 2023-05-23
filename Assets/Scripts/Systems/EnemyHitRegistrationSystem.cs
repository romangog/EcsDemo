using Leopotam.Ecs;
using UnityEngine;

public class EnemyHitRegistrationSystem : IEcsRunSystem
{
    private EcsFilter<
        HitRegisterRequest,
        MoveForwardComponent,
        TransformComponent,
        IgnoreEnemyComponent> _projecitlesFilter;

    private WeaponUpgradeLevels _weaponUpgradeLevels;
    private EcsWorld _world;

    public void Run()
    {
        foreach (var i in _projecitlesFilter)
        {
            ref var pistolShotEntity = ref _projecitlesFilter.GetEntity(i);
            ref var targetEntity = ref _projecitlesFilter.Get1(i).HitTarget;
            ref var emitterEntity = ref _projecitlesFilter.Get1(i).HitEmitter;
            ref var bulletMoveForward = ref _projecitlesFilter.Get2(i);
            ref var bulletTransform = ref _projecitlesFilter.Get3(i);
            ref var ignoreEnemy = ref _projecitlesFilter.Get4(i);

            if (ignoreEnemy.IgnoreEntity == targetEntity)
            {
                pistolShotEntity.Del<HitRegisterRequest>();
                continue;
            }

            if (!targetEntity.IsAlive()) continue;

            targetEntity.Get<HitByProjectileRequest>();
            float hitDamage = pistolShotEntity.Get<ProjectileDamageComponent>().Damage;
            targetEntity.Get<AccumulativeDamageComponent>().Damage += hitDamage;
            targetEntity.Get<HitImpactRequest>().PushForce +=
                bulletMoveForward.Direction * _weaponUpgradeLevels.GetThrowbackForceFromLevel();
        }
    }
}

public class ProjectileHitPenetrationSystem : IEcsRunSystem
{
    private EcsFilter<HitRegisterRequest, ProjectilePenetrationComponent> _projectilesPenetrationFilter;

    public void Run()
    {
        foreach (var i in _projectilesPenetrationFilter)
        {
            ref var projectileEntity = ref _projectilesPenetrationFilter.GetEntity(i);
            ref var penetration = ref _projectilesPenetrationFilter.Get2(i);

            if (penetration.PenetrationsLeft == 0)
            {
                projectileEntity.Get<DeathRequest>();
            }

            penetration.PenetrationsLeft--;
        }
    }
}

public class ProjectileHitVampirismSystem : IEcsRunSystem
{
    private EcsFilter<HitRegisterRequest, ProjectileVampirismComponent, ShooterComponent, ElementalParticlesComponent> _projectilesPenetrationFilter;

    private WeaponUpgradeLevels _weaponUpgrades;
    public void Run()
    {
        foreach (var i in _projectilesPenetrationFilter)
        {
            ref var projectileEntity = ref _projectilesPenetrationFilter.GetEntity(i);
            ref var vampirism = ref _projectilesPenetrationFilter.Get2(i);
            ref var shooter = ref _projectilesPenetrationFilter.Get3(i);
            ref var particles = ref _projectilesPenetrationFilter.Get4(i);

            shooter.ShooterEntity.Get<AccumulativeHealComponent>().Heal += _weaponUpgrades.GetVampirismHealFromLevel();
        }
    }
}





