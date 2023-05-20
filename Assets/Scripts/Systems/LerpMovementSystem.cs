using UnityEngine;
using Leopotam.Ecs;

public class LerpMovementSystem : IEcsRunSystem
{
    private EcsFilter<TransformComponent, LerpMovingComponent> _lerpMovingFilter;

    public void Run()
    {
        foreach (var i in _lerpMovingFilter)
        {
            ref var entity = ref _lerpMovingFilter.GetEntity(i);
            ref var transform = ref _lerpMovingFilter.Get1(i);
            ref var lerpMoving = ref _lerpMovingFilter.Get2(i);

            float distance = Vector3.Distance(lerpMoving.StartPoint, lerpMoving.Target.position);
            float time = distance / lerpMoving.Speed;
            lerpMoving.CurrentT = Mathf.MoveTowards(lerpMoving.CurrentT, 1f, Time.deltaTime / time);
            transform.Transform.position = Vector3.Lerp(lerpMoving.StartPoint, lerpMoving.Target.position, lerpMoving.CurrentT);
            if (lerpMoving.CurrentT == 1f)
            {
                entity.Get<AccountRequest>();
                entity.Del<LerpMovingComponent>();
            }
        }
    }
}



