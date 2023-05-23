using Leopotam.Ecs;

public class PlayerLevelProgressBarSystem : IEcsRunSystem
{
    private EcsFilter<PlayerLevelComponent, ChangeXpRequest> _playerXpChangeFilter;
    private EcsFilter<EntityReferenceComponent, ImageComponent, PlayerLevelProgressBarTag> _playerLevelProgressBarsFilter;

    public void Run()
    {
        foreach (var i in _playerXpChangeFilter)
        {
            ref var playerEntity = ref _playerXpChangeFilter.GetEntity(i);
            ref var playerLevel = ref _playerXpChangeFilter.Get1(i);
            foreach (var j in _playerLevelProgressBarsFilter)
            {
                ref var entityReference = ref _playerLevelProgressBarsFilter.Get1(j);
                if (entityReference.RootEntity != playerEntity) return;
                ref var image = ref _playerLevelProgressBarsFilter.Get2(j);

                image.Image.fillAmount = playerLevel.PlayerCurrentXP / (float)playerLevel.NextLevelXP;
            }
        }
    }
}






