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
        AnimationsIDs.Initialize();

        _world = new EcsWorld();
        _systems = new EcsSystems(_world)
            .Add(new InitializeEntityReferencesSystem())
            .Add(new EnemyThrowbackSystem())
            .Add(new PlayerCentricMovementSystem())
            .Add(new PlayerInputMovementSystem())
            .Add(new PistolFireSystem())
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
            .OneFrame<DeathRequest>()
            .OneFrame<HitImpactRequest>()
            .Inject(_prefabs)
            .Inject(_gameSettings)
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
}
