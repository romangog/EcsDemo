using Leopotam.Ecs;

public class EnemyIceHitSystem : IEcsRunSystem
{
    private EcsFilter<HitByProjectileRequest, EnemyTag> _hitEnemiesFilter;

    private WeaponUpgradeLevels _weaponUpgrades;

    public void Run()
    {
        if (_weaponUpgrades.IceLevel == 0) return;
        foreach (var i in _hitEnemiesFilter)
        {
            ref var enemyEntity = ref _hitEnemiesFilter.GetEntity(i);
            enemyEntity.Get<CatchIceRequest>();
        }
    }
}




