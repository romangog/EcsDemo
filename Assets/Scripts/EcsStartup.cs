using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Voody.UniLeo;
using Zenject;

public class EcsStartup : MonoBehaviour
{
    private Prefabs _prefabs;

    EcsWorld _world;
    EcsSystems _systems;

    [Inject]
    private void Construct(
        Prefabs prefabs)
    {
        _prefabs = prefabs;
    }

    void Start()
    {
        _world = new EcsWorld();
        _systems = new EcsSystems(_world)
            .OneFrame<EntityRootRequest>()
            .OneFrame<EntityChildRequest>()
            .Add(new InitializeEntityReferencesSystem())
            .Add(new PlayerCentricMovementSystem())
            .Add(new PlayerInputMovementSystem())
            .Add(new PistolFireSystem())
            .Add(new PlayerInvulSystem())
            .Add(new PlayerHealthSystem())
            .Add(new EnemyHealthSystem())
            .Add(new HealthbarSystem())
            .Add(new MoveInOneDirectionSystem())
            .Add(new EnemySpawnSystem())
            .Inject(_prefabs)
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
