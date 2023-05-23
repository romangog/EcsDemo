using UnityEngine;
using Leopotam.Ecs;

public class PlayerXpLevelUpSystem : IEcsRunSystem
{
    private EcsFilter<PlayerLevelComponent, ReachedNextLevelRequest> _reachedLevelFilter;
    public void Run()
    {
        foreach (var i in _reachedLevelFilter)
        {
            ref var levelComponent = ref _reachedLevelFilter.Get1(i);

            levelComponent.PlayerLevel++;
            levelComponent.PlayerCurrentXP -= levelComponent.NextLevelXP;
            levelComponent.NextLevelXP = Mathf.CeilToInt(levelComponent.NextLevelXP * 1.1f);
        }
    }
}






