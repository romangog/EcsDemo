using Leopotam.Ecs;

public class KillExplosionEntitySystem : IEcsRunSystem
{
    private EcsFilter<ExplosionTag> _explosionsFilter;

    public void Run()
    {
        foreach (var i in _explosionsFilter)
        {
            ref var entity = ref _explosionsFilter.GetEntity(i);

            entity.Destroy();
        }
    }
}



