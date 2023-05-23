using Leopotam.Ecs;

public class UiEventsClearSystem : IEcsRunSystem
{
    private EcsFilter<ButtonClickedTag> _clicks;
    public void Run()
    {
        foreach (var i in _clicks)
        {
            ref var clickEntity = ref _clicks.GetEntity(i);
            clickEntity.Del<ButtonClickedTag>();
        }
    }
}






