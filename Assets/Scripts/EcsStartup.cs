using Leopotam.Ecs;
using Leopotam.Ecs.Ui.Systems;
using UnityEngine;
using Voody.UniLeo;
using Zenject;

public class EcsStartup : MonoBehaviour
{
    private Prefabs _prefabs;
    private GameSettings _gameSettings;
    private EcsWorld _world;
    private EcsSystems _updateSystems;
    private EcsSystems _fixedSystems;
    private WeaponUpgradeLevels _weaponUpgradeLevels;
    private LevelData _levelData;
    private BulletSpawner _bulletSpawner;
    private IObjectPool<Projectile> _projectilePool;
    private IObjectPool<Gem> _gemsPool;
    private EcsUiEmitter _uiEmitter;

    [Inject]
    private void Construct(
        Prefabs prefabs,
        GameSettings gameSettings,
        EcsWorld world,
        WeaponUpgradeLevels weaponUpgradeLevels,
        BulletSpawner bulletSpawner,
        LevelData levelData,
        IObjectPool<Projectile> projectilePool,
        IObjectPool<Gem> gemsPool,
        EcsUiEmitter uiEmitter)
    {
        _prefabs = prefabs;
        _gameSettings = gameSettings;
        _world = world;
        _weaponUpgradeLevels = weaponUpgradeLevels;
        _bulletSpawner = bulletSpawner;
        _levelData = levelData;
        _projectilePool = projectilePool;
        _gemsPool = gemsPool;
        _uiEmitter = uiEmitter;
        Initialize();
    }

    void Initialize()
    {

        //// Adjust SpreadComponent - TURN INTO SYSTEM
        //ref var pistolShotMoveForward = ref entity.Get<MoveForwardComponent>();
        //pistolShotMoveForward.Direction = buleltSpawnMoveForward.Direction;
        //pistolShot.transform.rotation = Quaternion.LookRotation(Vector3.forward, pistolShotMoveForward.Direction);
        AnimationsIDs.Initialize();
        _fixedSystems = new EcsSystems(_world)
            .Add(new MoveInOneDirectionSystem())
            .Add(new PuddleMainEffectSystem())
                .Add(new PuddleFireEffectSystem())
                .Add(new PuddleIceEffectSystem())
                .Add(new PuddleLightningEffectSystem())
            .Add(new PlayerInputMovementSystem())
            .Add(new PlayerCentricMovementSystem())
            .Add(new PlayerCentricMovementSystem())
            .Add(new LightningFxPositionsSystem())
            .Inject(_gameSettings)
            .Inject(_weaponUpgradeLevels);

        _updateSystems = new EcsSystems(_world)
            .Add(new InitializeEntityReferencesSystem())

            // Projectie OnHit
            .Add(new EnemyHitRegistrationSystem())
                .Add(new FragmentationSpawnSystem())
                .Add(new EnemyFireHitSystem())
                .Add(new EnemyIceHitSystem())
                .Add(new LightningEmmisionSystem())
                .OneFrame<HitByProjectileRequest>()

            .Add(new ProjectilesFinalDeathSystem())
            .Add(new GiveGameObjectNameSystem())
                .OneFrame<GiveGameObjectNameRequest>()

            .Add(new EnemyCatchFireSystem())
            .OneFrame<CatchFireRequest>()
            .Add(new EnemyCatchIceSystem())
            .OneFrame<CatchIceRequest>()
            .Add(new EnemyGetLightningHittedSystem())
            .OneFrame<GetHitByLightningRequest>()

            .Add(new GemCollectionSystem())
            .Add(new GemCollectedAccountSystem())
            .Add(new EnemyThrowbackSystem())
            .Add(new WeaponFireControlSystem())
            .Add(new FireClosestEnemiesSystem())

            // Player Xp
            .Add(new PlayerXpSystem())
            .Add(new PlayerXpLevelUpSystem())
            // Player Xp : UI
            .Add(new PlayerLevelLabelSystem())
            .Add(new PlayerLevelProgressBarSystem())
            .Add(new PlayerLevelUpShowUpgradeScreenSystem())
            .Add(new UpgradeChooseClickedSystem())
                .OneFrame<ChangeXpRequest>()
                .OneFrame<ReachedNextLevelRequest>()


            .Add(new LerpMovementSystem())

            .Add(new EnemyOnFireSystem())
            .Add(new EnemyOnIceSystem())

            .Add(new BulletSpawnSystem())

            // Projectile OnShot Upgrade Adjustment
            .Add(new ProjectileSpeedLevelSystem())
            .Add(new ProjectileSpreadLevelSystem())
            .Add(new ProjectileDamageLevelSystem())
            .Add(new ProjectileSizeLevelSystem())
            .Add(new ProjectilePenetrationLevelSystem())
            .Add(new ProjectileSetOnFireSystem())
            .Add(new ProjectileSetOnIceSystem())

            .Add(new PlayerInvulSystem())
            .Add(new PlayerHealthSystem())
            .Add(new EnemyHitImpactSystem())
            .Add(new EnemyHitHighlightSystem())

            .Add(new EnemyIceDamageMultiplicationSystem())

            .Add(new EnemyHealthSystem())
            .Add(new EnemyDeathSystem())
                .Add(new SpawnGemsSystem())
            .Add(new ChildEntitiesDestroySystem())
            .Add(new HealthbarSystem())


            // Projectile Movement Upgrade Adjustment
            .Add(new ProjectileAutoAimLevelSystem())

            .Add(new MoveInOneDirectionSystem())
            .Add(new EnemyDeadDisableSystem())
            .Add(new EnemySpawnSystem())

            .Add(new LightningFxSpawnSystem())

            .Add(new ProjectileLifeEndSystem())
            .Add(new PuddleLifeEndSystem())
            .Add(new LightningLifeSystem())
            // Projectile OnDeath Upgrade Adjustment
            .Add(new ProjectileExplosionLevelSystem())
                .Add(new FireExplosionSystem())
                .Add(new IceExplosionSystem())
            .Add(new ProjectilePuddleSpawnLevelSystem())
                .Add(new PuddleSpawnElementalEffects())

            .Add(new SetColorSystem())
            .OneFrame<SetBaseColorRequest>()
            .Add(new KillExplosionEntitySystem())
            //.Add(new ProjectilesDestroyDeathSystem())
            .Add(new ProjectilesPoolDeathSystem())
            .Add(new UiEventsClearSystem())
            .OneFrame<DeathRequest>()
            .OneFrame<LightningSpawnRequest>()
            .OneFrame<OnSpawnRequest>()
            .OneFrame<SpawnBulletsRequest>()
            .OneFrame<HitImpactRequest>()
            .OneFrame<ProjectileShotRequest>()
            .OneFrame<ProjectileFinalDeathRequest>()
            .OneFrame<HitRegisterRequest>()
            .Inject(_prefabs)
            .Inject(_gameSettings)
            .Inject(_weaponUpgradeLevels)
            .Inject(_bulletSpawner)
            .Inject(_levelData)
            .Inject(_projectilePool)
            .Inject(_gemsPool)
            .InjectUi(_uiEmitter)
            .ConvertScene();


        _updateSystems.Init();
        _fixedSystems.Init();
    }

    void Update()
    {
        _updateSystems.Run();
    }

    private void FixedUpdate()
    {
        _fixedSystems.Run();
    }

    void OnDestroy()
    {
        _updateSystems.Destroy();
        _fixedSystems.Destroy();
        _world.Destroy();
    }

    private void OnGUI()
    {
        LeftUppperBlock();
        RightUpperBlock();

        void LeftUppperBlock()
        {
            Vector2 pos = new Vector2(0, 0);
            Vector2 size = new Vector2(100, 20);
            if (GUI.Button(new Rect(pos, size), "Speed " + _weaponUpgradeLevels.SpeedLevel.ToString()))
            {
                _weaponUpgradeLevels.SpeedLevel.Updgrade();
            }
            pos += Vector2.up * 20;
            if (GUI.Button(new Rect(pos, size), "Damage " + _weaponUpgradeLevels.DamageLevel.ToString()))
            {
                _weaponUpgradeLevels.DamageLevel.Updgrade();
            }
            pos += Vector2.up * 20;
            if (GUI.Button(new Rect(pos, size), "FireRate " + _weaponUpgradeLevels.FireRateLevel.ToString()))
            {
                _weaponUpgradeLevels.FireRateLevel.Updgrade();
            }
            pos += Vector2.up * 20;
            if (GUI.Button(new Rect(pos, size), "Size " + _weaponUpgradeLevels.ProjectileSizeLevel.ToString()))
            {
                _weaponUpgradeLevels.ProjectileSizeLevel.Updgrade();
            }
            pos += Vector2.up * 20;
            if (GUI.Button(new Rect(pos, size), "Penetration " + _weaponUpgradeLevels.PenetrationLevel.ToString()))
            {
                _weaponUpgradeLevels.PenetrationLevel.Updgrade();
            }
            pos += Vector2.up * 20;
            if (GUI.Button(new Rect(pos, size), "Spread " + _weaponUpgradeLevels.SpreadLevel.ToString()))
            {
                _weaponUpgradeLevels.SpreadLevel.Updgrade();
            }
            pos += Vector2.up * 20;
            if (GUI.Button(new Rect(pos, size), "Multiplier " + _weaponUpgradeLevels.ProjectileMultiplierLevel.ToString()))
            {
                _weaponUpgradeLevels.ProjectileMultiplierLevel.Updgrade();
            }
            pos += Vector2.up * 20;
            if (GUI.Button(new Rect(pos, size), "Fragmentation " + _weaponUpgradeLevels.FragmentationLevel.ToString()))
            {
                _weaponUpgradeLevels.FragmentationLevel.Updgrade();
            }
            pos += Vector2.up * 20;
            if (GUI.Button(new Rect(pos, size), "AutoAim " + _weaponUpgradeLevels.AutoAimLevel.ToString()))
            {
                _weaponUpgradeLevels.AutoAimLevel.Updgrade();
            }
            pos += Vector2.up * 20;
            if (GUI.Button(new Rect(pos, size), "Explosion " + _weaponUpgradeLevels.ExplosionLevel.ToString()))
            {
                _weaponUpgradeLevels.ExplosionLevel.Updgrade();
            }
            pos += Vector2.up * 20;
            if (GUI.Button(new Rect(pos, size), "Puddle " + _weaponUpgradeLevels.PuddleLevel.ToString()))
            {
                _weaponUpgradeLevels.PuddleLevel.Updgrade();
            }
            pos += Vector2.up * 20;
            if (GUI.Button(new Rect(pos, size), "Fire " + _weaponUpgradeLevels.FireLevel.ToString()))
            {
                _weaponUpgradeLevels.FireLevel.Updgrade();
            }
            pos += Vector2.up * 20;

            if (GUI.Button(new Rect(pos, size), "Ice " + _weaponUpgradeLevels.IceLevel.ToString()))
            {
                _weaponUpgradeLevels.IceLevel.Updgrade();
            }
            pos += Vector2.up * 20;

            if (GUI.Button(new Rect(pos, size), "Lightning " + _weaponUpgradeLevels.LightningLevel.ToString()))
            {
                _weaponUpgradeLevels.LightningLevel.Updgrade();
            }
            pos += Vector2.up * 20;
        }

        void RightUpperBlock()
        {
            Vector2 size = new Vector2(100, 20);
            Vector2 pos = new Vector2(Screen.width - size.x, 0);
            if (GUI.Button(new Rect(pos, size), "EnemyLevel " + _levelData.EnemyLevel.ToString()))
            {
                _levelData.EnemyLevel++;
            }
            pos += Vector2.up * 20;
        }

    }
}
