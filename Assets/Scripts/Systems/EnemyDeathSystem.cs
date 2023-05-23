using Leopotam.Ecs;

public class EnemyDeathSystem : IEcsRunSystem
{
    private EcsFilter<EnemyTag, DeathRequest, AnimatorComponent, EnemyHitboxComponent> _enemyDeathFilter;

    private EcsWorld _world;

    public void Run()
    {
        foreach (var i in _enemyDeathFilter)
        {
            ref var entity = ref _enemyDeathFilter.GetEntity(i);
            ref var animator = ref _enemyDeathFilter.Get3(i);
            ref var enemyHitbox = ref _enemyDeathFilter.Get4(i);
            animator.Animator.SetTrigger(AnimationsIDs.Die);
            enemyHitbox.EnemyHitbox.enabled = false;
            entity.Get<DeadTag>();

            ref var timerAction = ref entity.Get<TimerComponent>();
            timerAction.Timer.Set(0.3f);
        }
    }

}

public class PlayerDeathScreenShowSystem : IEcsRunSystem
{
    private EcsFilter<PlayerTag, DeadTag, PlayerDeathExitTimer> _playerDeathFilter;

    private EcsFilter<DeathScreenComponent>.Exclude<ShownTag> _deathScreenFilter;

    public void Run()
    {
        if (_playerDeathFilter.GetEntitiesCount() == 0
            || _deathScreenFilter.GetEntitiesCount() == 0) return;
        ref var playerEntity = ref _playerDeathFilter.GetEntity(0);
        ref var timer = ref _playerDeathFilter.Get3(0);
        timer.Timer.Update();
        if(timer.Timer.IsOver)
        {
            ref var deathScreenEntity = ref _deathScreenFilter.GetEntity(0);
            ref var deathScreen = ref _deathScreenFilter.Get1(0);

            deathScreen.View.SetActive(true);
            deathScreenEntity.Get<ShownTag>();
            playerEntity.Del<PlayerDeathExitTimer>();
        }
    }
}

