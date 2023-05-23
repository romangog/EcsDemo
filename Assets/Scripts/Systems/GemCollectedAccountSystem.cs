using Leopotam.Ecs;

public class GemCollectedAccountSystem : IEcsRunSystem
{
    private EcsFilter<GemDataComponent, GameObjectComponent, CollectedComponent, AccountRequest, GemTag> _lerpMovingFilter;
    //private EcsFilter<PlayerLevelComponent> _levelFilter;
    private LevelData _levelData;
    private IObjectPool<Gem> _gemsPool;

    public void Run()
    {
        foreach (var i in _lerpMovingFilter)
        {
            ref var entity = ref _lerpMovingFilter.GetEntity(i);
            ref var gemData = ref _lerpMovingFilter.Get1(i);
            ref var gameObject = ref _lerpMovingFilter.Get2(i);
            ref var collected = ref _lerpMovingFilter.Get3(i);

            collected.CollectedEntity.Get<ChangeXpRequest>().Delta += gemData.Value;

            _gemsPool.Return(gameObject.GameObject);

            entity.Destroy();
        }
    }
}






