using UnityEngine;
using Leopotam.Ecs;
using UnityEngine.SceneManagement;

public class RestartGameSystem : IEcsRunSystem
{
    private EcsFilter<DeathScreenComponent, ButtonClickedTag> _deathScreenRestartClickedFilter;
    private EcsFilter<EnemyTag, GameObjectComponent> _enemiesFilter;
    private EcsFilter<ProjectileFragmentTag, GameObjectComponent> _projectilesFilter;
    private EcsFilter<ExplosionTag, GameObjectComponent> _explosionsFilter;
    private EcsFilter<PuddleTag, GameObjectComponent> _puddleFilter;
    private EcsFilter<LightningFxTag, GameObjectComponent> _lightningFilter;
    private EcsFilter<GemTag, GameObjectComponent> _gemsFilter;
    private EcsFilter<PlayerTag> _playerLevelFilter;

    private WeaponUpgradeLevels _weaponUpgrades;
    private IObjectPool<Gem> _gemsPool;

    public void Run()
    {
        if (_deathScreenRestartClickedFilter.GetEntitiesCount() == 0)
            return;
        SceneManager.LoadScene(0);
        return;
        ref var deathScreen = ref _deathScreenRestartClickedFilter.Get1(0);
        deathScreen.View.SetActive(false);
        Debug.Log("_playerLevelFilter: " + _playerLevelFilter.GetEntitiesCount());
        ref var playerEntity = ref _playerLevelFilter.GetEntity(0);
        ref var playerLevel = ref playerEntity.Get<PlayerLevelComponent>();
        playerLevel.Reinitialize();
        ref var playerHealth = ref playerEntity.Get<HealthComponent>();
        playerHealth.Reinitialize();
        ref var playerTransform = ref playerEntity.Get<TransformComponent>();
        playerTransform.Transform.position = Vector3.zero;

        playerEntity.Del<DeadTag>();
        Debug.Log("Player DEL deadTag");
        playerEntity.Del<PlayerInvulComponent>();
        playerEntity.Del<AccumulativeDamageComponent>();
        playerEntity.Get<AnimatorComponent>().Animator.Play("Idle");
        playerEntity.Get<AnimatorComponent>().Animator.ResetTrigger(AnimationsIDs.Die);
        _weaponUpgrades.Reinitialize();
        foreach (var i in _enemiesFilter)
        {
            ref var enemyEntity = ref _enemiesFilter.GetEntity(i);
            ref var enemyGO = ref _enemiesFilter.Get2(i);

            GameObject.Destroy(enemyGO.GameObject);
            enemyEntity.Destroy();
        }

        foreach (var i in _projectilesFilter)
        {
            ref var projectileEntity = ref _projectilesFilter.GetEntity(i);
            ref var projectileGO = ref _projectilesFilter.Get2(i);

            GameObject.Destroy(projectileGO.GameObject);
            projectileEntity.Destroy();
        }

        foreach (var i in _explosionsFilter)
        {
            ref var entity = ref _explosionsFilter.GetEntity(i);
            ref var entityGO = ref _explosionsFilter.Get2(i);

            GameObject.Destroy(entityGO.GameObject);
            entity.Destroy();
        }

        foreach (var i in _puddleFilter)
        {
            ref var entity = ref _puddleFilter.GetEntity(i);
            ref var entityGO = ref _puddleFilter.Get2(i);

            GameObject.Destroy(entityGO.GameObject);
            entity.Destroy();
        }

        foreach (var i in _lightningFilter)
        {
            ref var entity = ref _lightningFilter.GetEntity(i);
            ref var entityGO = ref _lightningFilter.Get2(i);

            GameObject.Destroy(entityGO.GameObject);
            entity.Destroy();
        }

        foreach (var i in _gemsFilter)
        {
            ref var entity = ref _gemsFilter.GetEntity(i);
            ref var entityGO = ref _gemsFilter.Get2(i);
            _gemsPool.Return(entityGO.GameObject);
            entity.Destroy();
        }
    }
}

