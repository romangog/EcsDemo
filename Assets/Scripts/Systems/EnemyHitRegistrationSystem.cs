using Leopotam.Ecs;
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

            targetEntity.Get<HitByProjectileRequest>();
            targetEntity.Get<AccumulativeDamageComponent>().Damage += pistolShotEntity.Get<ProjectileDamageComponent>().Damage;
            targetEntity.Get<HitImpactRequest>().PushForce +=
                bulletMoveForward.Direction * _weaponUpgradeLevels.GetThrowbackForceFromLevel();

            if (bulletPenetration.PenetrationsLeft == 0)
            {
                pistolShotEntity.Get<DeathRequest>();
            }
            bulletPenetration.PenetrationsLeft--;
        }
    }
}


