using Leopotam.Ecs;
using UnityEngine;

public class PlayerDeathSystem : IEcsRunSystem
{
    private EcsFilter<PlayerTag, DeathRequest, AnimatorComponent> _playerDeathFilter;

    public void Run()
    {
        foreach (var i in _playerDeathFilter)
        {
            ref var entity = ref _playerDeathFilter.GetEntity(i);
            ref var animator = ref _playerDeathFilter.Get3(i);
            animator.Animator.SetTrigger(AnimationsIDs.Die);
            entity.Get<DeadTag>();
            Debug.Log("Player got deadTag");
            ref var timerAction = ref entity.Get<PlayerDeathExitTimer>();
            timerAction.Timer.Set(1f);
        }
    }
}

