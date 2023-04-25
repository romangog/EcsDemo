using Leopotam.Ecs;
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

    [Inject]
    private void Construct(
        Prefabs prefabs,
        GameSettings gameSettings,
        EcsWorld world,
        WeaponUpgradeLevels weaponUpgradeLevels,
        BulletSpawner bulletSpawner,
        LevelData levelData)
    {
        _prefabs = prefabs;
        _gameSettings = gameSettings;
        _world = world;
        _weaponUpgradeLevels = weaponUpgradeLevels;
        _bulletSpawner = bulletSpawner;
        _levelData = levelData;

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
            .Add(new PlayerCentricMovementSystem());

        _updateSystems = new EcsSystems(_world)
            .Add(new InitializeEntityReferencesSystem())
            .Add(new EnemyHitRegistrationSystem())
            .Add(new FragmentationSpawnSystem())
            .Add(new EnemyThrowbackSystem())
            .Add(new PlayerInputMovementSystem())
            .Add(new WeaponFireControlSystem())

            .Add(new BulletSpawnSystem())

            // Projectile OnShot Upgrade Adjustment
            .Add(new ProjectileSpeedLevelSystem())         // Speed
            .Add(new ProjectileSpreadLevelSystem())        // Spread
            .Add(new ProjectileDamageLevelSystem())        // Damage
            .Add(new ProjectileSizeLevelSystem())          // Size
            .Add(new ProjectilePenetrationLevelSystem())   // Penetration
            .Add(new ProjectileFragmentationLevelSystem())   // Penetration

            .Add(new PlayerInvulSystem())
            .Add(new PlayerHealthSystem())
            .Add(new EnemyHitImpactSystem())
            .Add(new EnemyHitHighlightSystem())
            .Add(new EnemyHealthSystem())
            .Add(new EnemyDeathSystem())
            .Add(new ChildEntitiesDestroySystem())
            .Add(new HealthbarSystem())

            // Projectile Movement Upgrade Adjustment
            .Add(new ProjectileAutoAimLevelSystem())

            .Add(new MoveInOneDirectionSystem())
            .Add(new EnemyDeadDisableSystem())
            .Add(new EnemySpawnSystem())
            .Add(new ProjectileLifeEndSystem())

            // Projectile OnDeath Upgrade Adjustment
            .Add(new ProjectileExplosionLevelSystem())

            .Add(new ProjectilesDeathSystem())
            .OneFrame<DeathRequest>()
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
                _weaponUpgradeLevels.SpeedLevel++;
            }
            pos += Vector2.up * 20;
            if (GUI.Button(new Rect(pos, size), "Damage " + _weaponUpgradeLevels.DamageLevel.ToString()))
            {
                _weaponUpgradeLevels.DamageLevel++;
            }
            pos += Vector2.up * 20;
            if (GUI.Button(new Rect(pos, size), "FireRate " + _weaponUpgradeLevels.FireRateLevel.ToString()))
            {
                _weaponUpgradeLevels.FireRateLevel++;
            }
            pos += Vector2.up * 20;
            if (GUI.Button(new Rect(pos, size), "Size " + _weaponUpgradeLevels.ProjectileSizeLevel.ToString()))
            {
                _weaponUpgradeLevels.ProjectileSizeLevel++;
            }
            pos += Vector2.up * 20;
            if (GUI.Button(new Rect(pos, size), "Penetration " + _weaponUpgradeLevels.PenetrationLevel.ToString()))
            {
                _weaponUpgradeLevels.PenetrationLevel++;
            }
            pos += Vector2.up * 20;
            if (GUI.Button(new Rect(pos, size), "Spread " + _weaponUpgradeLevels.SpreadLevel.ToString()))
            {
                _weaponUpgradeLevels.SpreadLevel++;
            }
            pos += Vector2.up * 20;
            if (GUI.Button(new Rect(pos, size), "Multiplier " + _weaponUpgradeLevels.ProjectileMultiplierLevel.ToString()))
            {
                _weaponUpgradeLevels.ProjectileMultiplierLevel++;
            }
            pos += Vector2.up * 20;
            if (GUI.Button(new Rect(pos, size), "Fragmentation " + _weaponUpgradeLevels.FragmentationLevel.ToString()))
            {
                _weaponUpgradeLevels.FragmentationLevel++;
            }
            pos += Vector2.up * 20;
            if (GUI.Button(new Rect(pos, size), "AutoAim " + _weaponUpgradeLevels.AutoAimLevel.ToString()))
            {
                _weaponUpgradeLevels.AutoAimLevel++;
            }
            pos += Vector2.up * 20;
            if (GUI.Button(new Rect(pos, size), "Explosion " + _weaponUpgradeLevels.ExplosionLevel.ToString()))
            {
                _weaponUpgradeLevels.ExplosionLevel++;
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
