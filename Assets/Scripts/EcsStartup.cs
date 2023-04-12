using Leopotam.Ecs;
using UnityEngine;
using Voody.UniLeo;
using Zenject;

public class EcsStartup : MonoBehaviour
{
    private Prefabs _prefabs;
    private GameSettings _gameSettings;
    private EcsWorld _world;
    private EcsSystems _systems;
    private WeaponUpgradeLevels _weaponUpgradeLevels;

    [Inject]
    private void Construct(
        Prefabs prefabs,
        GameSettings gameSettings)
    {
        _prefabs = prefabs;
        _gameSettings = gameSettings;
    }

    void Start()
    {

        //// Adjust SpreadComponent - TURN INTO SYSTEM
        //ref var pistolShotMoveForward = ref entity.Get<MoveForwardComponent>();
        //pistolShotMoveForward.Direction = buleltSpawnMoveForward.Direction;
        //pistolShot.transform.rotation = Quaternion.LookRotation(Vector3.forward, pistolShotMoveForward.Direction);

        AnimationsIDs.Initialize();
        _world = new EcsWorld();
        _weaponUpgradeLevels = new WeaponUpgradeLevels();
        _systems = new EcsSystems(_world)
            .Add(new InitializeEntityReferencesSystem())
            .Add(new EnemyThrowbackSystem())
            .Add(new PlayerCentricMovementSystem())
            .Add(new PlayerInputMovementSystem())
            .Add(new WeaponFireControlSystem())
            .Add(new BulletSpawnSystem())
            .Add(new ProjectileSpeedLevelSystem())
            .Add(new ProjectileSpreadLevelSystem())
            .Add(new ProjectileDamageLevelSystem())
            .Add(new PlayerInvulSystem())
            .Add(new PlayerHealthSystem())
            .Add(new EnemyHitImpactSystem())
            .Add(new EnemyHitHighlightSystem())
            .Add(new EnemyHealthSystem())
            .Add(new EnemyDeathSystem())
            .Add(new ChildEntitiesDestroySystem())
            .Add(new HealthbarSystem())
            .Add(new EnemyDeadDisableSystem())
            .Add(new MoveInOneDirectionSystem())
            .Add(new EnemySpawnSystem())
            .Add(new ProjectileLifeEndSystem())
            .Add(new ProjectilesDeathSystem())
            .OneFrame<DeathRequest>()
            .OneFrame<SpawnBulletsRequest>()
            .OneFrame<HitImpactRequest>()
            .OneFrame<ProjectileShotRequest>()
            .Inject(_prefabs)
            .Inject(_gameSettings)
            .Inject(_weaponUpgradeLevels)
            .ConvertScene();
        _systems.Init();
    }

    void Update()
    {
        _systems.Run();
    }

    void OnDestroy()
    {
        _systems.Destroy();
        _world.Destroy();
    }

    private void OnGUI()
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
    }
}
