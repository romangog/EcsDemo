using Leopotam.Ecs;

public class PlayerLevelUpShowUpgradeScreenSystem : IEcsRunSystem
{
    private EcsFilter<ReachedNextLevelRequest> _nextLevelReachedFilter;
    private EcsFilter<PlayerLevelUpUpgradeScreenComponent>.Exclude<ShownTag> _levelUpUpgradeScreenFilter;
    private EcsFilter<UpgradeViewComponent>.Exclude<ShownTag> _upgradeButtons;
    private WeaponUpgradeLevels _weaponUpgrades;

    public void Run()
    {
        if (_nextLevelReachedFilter.IsEmpty()) return;
        ref var upgradeScreen = ref _levelUpUpgradeScreenFilter.GetEntity(0);
        ref var upgradeScreenView = ref _levelUpUpgradeScreenFilter.Get1(0);
        upgradeScreen.Get<ShownTag>();
        upgradeScreenView.LevelUpScreen.View.SetActive(true);

        foreach (var i in _upgradeButtons)
        {
            ref var entity = ref _upgradeButtons.GetEntity(i);
            ref var viewComponent = ref _upgradeButtons.Get1(i);
            var upgrade = _weaponUpgrades.WeaponLevels[UnityEngine.Random.Range(0, _weaponUpgrades.WeaponLevels.Count)];
            viewComponent.Icon.sprite = upgrade.Sprite;
            viewComponent.Description.text = upgrade.Name;
            viewComponent.WeaponLevel = upgrade;
            viewComponent.View.SetActive(true);
            entity.Get<ShownTag>();
        }
    }
}






