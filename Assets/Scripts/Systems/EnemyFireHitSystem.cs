using Leopotam.Ecs;

public class EnemyFireHitSystem : IEcsRunSystem
{
    private EcsFilter<HitByProjectileRequest, EnemyTag> _hitEnemiesFilter;

    private WeaponUpgradeLevels _weaponUpgrades;

    public void Run()
    {
        if (_weaponUpgrades.FireLevel == 0) return;
        foreach (var i in _hitEnemiesFilter)
        {
            ref var enemyEntity = ref _hitEnemiesFilter.GetEntity(i);
            enemyEntity.Get<CatchFireRequest>();
        }
    }
}


