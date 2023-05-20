using UnityEngine;
using Leopotam.Ecs;

public class SpawnGemsSystem : IEcsRunSystem
{
    private EcsFilter<TransformComponent, EnemyTag, DeathRequest> _dyingEnemiesFilter;

    private IObjectPool<Gem> _gemsPool;
    private EcsWorld _world;
    public void Run()
    {
        foreach (var i in _dyingEnemiesFilter)
        {
            ref var enemyTransform = ref _dyingEnemiesFilter.Get1(i);

            var gemEntity = _world.NewEntity();
            int id = gemEntity.GetInternalId();
            //Debug.Log("Create Entity: " + gemEntity.GetInternalId());
            Gem gem = _gemsPool.Get();
            gem.name = "Gem " + gemEntity.GetInternalId();
            gem.transform.position = enemyTransform.Transform.position;
            gem.GemTrigger.TriggerEnteredEvent += (collectedEntityReference) =>
            {

                
                OnGemTriggerEntered(ref gemEntity, collectedEntityReference);
                gem.GemTrigger.TriggerEnteredEvent = null;
            };

                gemEntity.Get<GemTag>();
            gemEntity.Get<GemDataComponent>().Value = 1;

            gemEntity.Get<TransformComponent>().Transform = gem.transform;
            gemEntity.Get<GameObjectComponent>().GameObject= gem.gameObject;
        }
    }

    private void OnGemTriggerEntered(ref EcsEntity gemEntity, EntityReference entityReference)
    {
        if (gemEntity.Has<CollectedComponent>()) return;
        ref var collected = ref gemEntity.Get<CollectRequest>();

        collected.CollectedTarget = entityReference.transform;
        collected.CollectedEntity = entityReference.Entity;
    }
}



