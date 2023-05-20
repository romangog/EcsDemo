using UnityEngine;
using Leopotam.Ecs;
using Leopotam.Ecs.Ui.Components;

public class GemCollectedAccountSystem : IEcsRunSystem
{
    private EcsFilter<GemDataComponent, GameObjectComponent, CollectedComponent, AccountRequest, GemTag> _lerpMovingFilter;
    //private EcsFilter<PlayerLevelComponent> _levelFilter;
    private LevelData _levelData;
    private IObjectPool<Gem> _gemsPool;

    public void Run()
    {
        foreach (var i in _lerpMovingFilter)
        {
            ref var entity = ref _lerpMovingFilter.GetEntity(i);
            ref var gemData = ref _lerpMovingFilter.Get1(i);
            ref var gameObject = ref _lerpMovingFilter.Get2(i);
            ref var collected = ref _lerpMovingFilter.Get3(i);

            collected.CollectedEntity.Get<ChangeXpRequest>().Delta += gemData.Value;

            _gemsPool.Return(gameObject.GameObject);

            entity.Destroy();
        }
    }
}

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
            levelComponent.NextLevelXP = Mathf.CeilToInt(levelComponent.NextLevelXP * 1.25f);
        }
    }
}

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
            viewComponent.Description.text = upgrade.GetDescription();
            viewComponent.WeaponLevel = upgrade;
            viewComponent.View.SetActive(true);
            entity.Get<ShownTag>();
        }
    }
}

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
        Debug.Log("Upgrade");
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
public class UiEventsClearSystem : IEcsRunSystem
{
    private EcsFilter<ButtonClickedTag> _clicks;
    public void Run()
    {
        foreach (var i in _clicks)
        {
            Debug.Log("Del");
            ref var clickEntity = ref _clicks.GetEntity(i);
            clickEntity.Del<ButtonClickedTag>();
        }
    }
}






