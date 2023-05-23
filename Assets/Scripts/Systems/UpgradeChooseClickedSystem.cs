using UnityEngine;
using Leopotam.Ecs;

public class UpgradeChooseClickedSystem : IEcsRunSystem
{
    private EcsFilter<UpgradeViewComponent, ButtonClickedTag> _upgradesClicked;
    private EcsFilter<UpgradeViewComponent, ShownTag> _upgradeButtons;
    private EcsFilter<PlayerLevelUpUpgradeScreenComponent, ShownTag> _levelUpUpgradeScreenFilter;

    public void Run()
    {
        if (_upgradesClicked.GetEntitiesCount() == 0) return;
        ref var upgradeScreen = ref _levelUpUpgradeScreenFilter.Get1(0);
        upgradeScreen.LevelUpScreen.View.SetActive(false);

        ref var upgradeView = ref _upgradesClicked.Get1(0);
        upgradeView.WeaponLevel++;
        foreach (var i in _upgradeButtons)
        {
            ref var buttonEntity = ref _upgradeButtons.GetEntity(i);
            ref var view = ref _upgradeButtons.Get1(i);
            view.View.SetActive(false);
            buttonEntity.Del<ShownTag>();
        }
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
    }
}






