using Leopotam.Ecs;

public class EnemyOnIceSystem : IEcsRunSystem
{
    private EcsFilter<SpeedMultiplierComponent, TargetInIceComponent, EnemyTag>.Exclude<DeadTag> _aliveEnemiesFilter;
    

    private GameSettings _gameSettings;

    public void Run()
    {
        foreach (var enemyId in _aliveEnemiesFilter)
        {
            ref var enemyEntity = ref _aliveEnemiesFilter.GetEntity(enemyId);
            ref var enemySpeedMultiplier = ref _aliveEnemiesFilter.Get1(enemyId);
            ref var enemyInIce = ref _aliveEnemiesFilter.Get2(enemyId);

            enemySpeedMultiplier.Multiplier *= 0;

            enemyInIce.IceTimer.Update();
            if (enemyInIce.IceTimer.IsOver)
            {
                enemyEntity.Del<TargetInIceComponent>();
                enemyEntity.Get<SetBaseColorRequest>().BaseColor = _gameSettings.BlackColor;
            }
        }
    }
}




