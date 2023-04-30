using Leopotam.Ecs;

public class EnemyOnFireSystem : IEcsRunSystem
{
    private EcsFilter<EnemyTag, TransformComponent, TargetOnFireComponent>.Exclude<DeadTag> _aliveEnemiesFilter;
    //private EcsFilter<EnemyTag, DeadTag, TargetOnFireComponent> _deadEnemiesFilter;

    public void Run()
    {
        foreach (var enemyId in _aliveEnemiesFilter)
        {
            ref var enemyEntity = ref _aliveEnemiesFilter.GetEntity(enemyId);
            ref var enemyTransform = ref _aliveEnemiesFilter.Get2(enemyId);
            ref var enemyOnFire = ref _aliveEnemiesFilter.Get3(enemyId);

            enemyOnFire.FireDamageTickTimer.Update();
            if (enemyOnFire.FireDamageTickTimer.IsOver)
            {
                enemyEntity.Get<AccumulativeDamageComponent>().Damage += enemyOnFire.DamagePerSec;
                enemyOnFire.FireDamageTickTimer.Set(1f);
            }

            enemyOnFire.FireTimer.Update();
            if (enemyOnFire.FireTimer.IsOver)
            {
                enemyEntity.Del<TargetOnFireComponent>();
                enemyEntity.Get<ElementalParticlesComponent>().FireFx.Stop();
            }

        }
    }
}




