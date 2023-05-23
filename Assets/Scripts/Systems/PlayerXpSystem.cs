using UnityEngine;
using Leopotam.Ecs;

public class PlayerXpSystem : IEcsRunSystem
{
    private EcsFilter<PlayerLevelComponent, ChangeXpRequest> _playerXpChangeFilter;

    public void Run()
    {
        foreach (var i in _playerXpChangeFilter)
        {
            ref var entity = ref _playerXpChangeFilter.GetEntity(i);
            ref var levelComponent = ref _playerXpChangeFilter.Get1(i);
            ref var changeXp = ref _playerXpChangeFilter.Get2(i);

            levelComponent.PlayerCurrentXP += changeXp.Delta;
            if (levelComponent.PlayerCurrentXP >= levelComponent.NextLevelXP)
            {
                entity.Get<ReachedNextLevelRequest>();
                Time.timeScale = 0f;
                Time.fixedDeltaTime = 0f;
            }

        }
    }
}






