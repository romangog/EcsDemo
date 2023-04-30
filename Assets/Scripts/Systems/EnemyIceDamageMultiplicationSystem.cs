using Leopotam.Ecs;

public class EnemyIceDamageMultiplicationSystem : IEcsRunSystem
{
    EcsFilter<AccumulativeDamageComponent, TargetInIceComponent, EnemyTag> _enemyDamageFilter;

    public void Run()
    {
        foreach (var i in _enemyDamageFilter)
        {
            ref var accumDamage = ref _enemyDamageFilter.Get1(i);
            ref var ice = ref _enemyDamageFilter.Get2(i);

            accumDamage.Damage *= ice.IceDamageMultiplier;
        }
    }
}
