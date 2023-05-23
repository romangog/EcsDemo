using Leopotam.Ecs;

public class PlayerLevelLabelSystem : IEcsRunSystem
{
    private EcsFilter<PlayerLevelComponent, ReachedNextLevelRequest> _reachedLevelFilter;
    private EcsFilter<TmpTextComponent, EntityReferenceComponent, PlayerLevelLabelTag> _levelLabelFilter;
    public void Run()
    {
        foreach (var i in _reachedLevelFilter)
        {
            ref var entity = ref _reachedLevelFilter.GetEntity(i);
            ref var levelComponent = ref _reachedLevelFilter.Get1(i);

            foreach (var j in _levelLabelFilter)
            {
                ref var rootEntity = ref _levelLabelFilter.Get2(j);
                if (rootEntity.RootEntity != entity) continue;

                ref var text = ref _levelLabelFilter.Get1(j);
                text.TextComponent.text = "LEVEL " + levelComponent.PlayerLevel;
            }
        }
    }
}






