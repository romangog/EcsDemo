using Leopotam.Ecs;

public class GemCollectionSystem : IEcsRunSystem
{
    private EcsFilter<TransformComponent, CollectRequest,GemTag>.Exclude<CollectedComponent> _gemsUnpickedFilter;

    private GameSettings _gameSettings;

    public void Run()
    {
        foreach (var i in _gemsUnpickedFilter)
        {
            ref var entity = ref _gemsUnpickedFilter.GetEntity(i);
            ref var transform = ref _gemsUnpickedFilter.Get1(i);
            ref var collectedRequest = ref _gemsUnpickedFilter.Get2(i);

            entity.Get<CollectedComponent>().CollectedEntity = collectedRequest.CollectedEntity;
            ref var lerpMoving = ref entity.Get<LerpMovingComponent>();
            lerpMoving.CurrentT = 0f;
            lerpMoving.Speed = _gameSettings.GemCollectionSpeed;
            lerpMoving.StartPoint = transform.Transform.position;
            lerpMoving.Target = collectedRequest.CollectedTarget;
        }
    }
}



