using UnityEngine;
using Leopotam.Ecs;

public class EnemyDeathSystem : IEcsRunSystem
{
    public EcsFilter<EnemyTag, DeathRequest, AnimatorComponent, EnemyHitboxComponent> _enemyDeathFilter;

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
