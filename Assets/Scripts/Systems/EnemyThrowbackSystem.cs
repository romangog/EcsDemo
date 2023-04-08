using Leopotam.Ecs;

public class EnemyThrowbackSystem : IEcsRunSystem
{
    EcsFilter<EnemyTag, HitThrowbackTimer> _hitThrowbackFilter;

    public void Run()
    {
        foreach (var id in _hitThrowbackFilter)
        {
            ref var throwback = ref _hitThrowbackFilter.Get2(id);
            ref var entity = ref _hitThrowbackFilter.GetEntity(id);
            throwback.Timer.Update();
            if (throwback.Timer.IsOver)
            {
                entity.Del<HitThrowbackTimer>();
            }
        }
    }
}
