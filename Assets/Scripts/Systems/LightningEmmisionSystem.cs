using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine;

public class LightningEmmisionSystem : IEcsRunSystem
{
    private EcsFilter<TransformComponent, HitByProjectileRequest, EnemyTag> _hitEnemiesFilter;

    private WeaponUpgradeLevels _weaponUpgrades;
    private GameSettings _gameSettings;
    private EcsWorld _world;
    private Prefabs _prefabs;

    public void Run()
    {
        if (_weaponUpgrades.LightningLevel == 0) return;

        foreach (var i in _hitEnemiesFilter)
        {
            ref var enemyEntity = ref _hitEnemiesFilter.GetEntity(i);
            ref var enemyTransform = ref _hitEnemiesFilter.Get1(i);
            enemyEntity.Get<GetHitByLightningRequest>();
            List<TransformComponent> targetsList = new List<TransformComponent>();
            targetsList.Add(enemyTransform);
            for (int j = 0; j < _weaponUpgrades.LightningLevel; j++)
            {
                RaycastHit2D[] hits = Physics2D.CircleCastAll(
                    enemyTransform.Transform.position,
                    _gameSettings.LightningReachRadius,
                    Vector3.zero,
                    0f,
                    _gameSettings.EnemyColliderLayerMask);
                if (hits.Length <= 1) break;
                foreach (var hit in hits)
                {
                    ref var targetEntity = ref hit.transform.GetComponent<EntityReference>().Entity;
                    if (!targetEntity.IsAlive() || targetEntity.Has<GetHitByLightningRequest>())
                    {
                        continue;
                    }
                    targetEntity.Get<GetHitByLightningRequest>();
                    enemyTransform = ref targetEntity.Get<TransformComponent>();
                    targetsList.Add(enemyTransform);
                    break;
                }
            }

            if (targetsList.Count < 2) continue;


            var lightningSpawn = _world.NewEntity();
            lightningSpawn.Get<LightningSpawnRequest>().Targets = targetsList;
        }
    }
}

public class GiveGameObjectNameSystem : IEcsRunSystem
{
    private EcsFilter<GameObjectComponent, GiveGameObjectNameRequest> _nameRequestsFilter;
    public void Run()
    {
        foreach (var i in _nameRequestsFilter)
        {
            ref var entity = ref _nameRequestsFilter.GetEntity(i);
            ref var gameObject = ref _nameRequestsFilter.Get1(i);
            ref var nameRequest = ref _nameRequestsFilter.Get2(i);

            gameObject.GameObject.name = nameRequest.BaseName + ' ' + entity.GetInternalId().ToString();
        }
    }
}


public class LightningFxSpawnSystem : IEcsRunSystem
{
    private EcsFilter<LightningSpawnRequest> _lightningRequestsFilter;

    private Prefabs _prefabs;

    public void Run()
    {
        foreach (var i in _lightningRequestsFilter)
        {
            ref var spawnRequest =ref _lightningRequestsFilter.Get1(i);
            ref var lightningFxEntity = ref _lightningRequestsFilter.GetEntity(i);

            LightningFX lightningGO = GameObject.Instantiate(_prefabs.LigntningChainFx, Vector3.zero, Quaternion.identity);
            lightningFxEntity.Get<LightningFxTag>();
            lightningFxEntity.Get<TimerComponent>().Timer.Set(0.5f);
            lightningFxEntity.Get<GameObjectComponent>().GameObject = lightningGO.gameObject;
            ref var lineRenderer = ref lightningFxEntity.Get<LineRendererComponent>();
            lineRenderer.LineRenderer = lightningGO.LineRendererComponent.LineRenderer;
            lineRenderer.LineRenderer.positionCount = spawnRequest.Targets.Count;
            ref var lightningTargets = ref lightningFxEntity.Get<AffectedTransformsComponent>();
            lightningTargets.Transforms = new System.Collections.Generic.List<TransformComponent>();

            for (int j = 0; j < spawnRequest.Targets.Count; j++)
            {
                lightningTargets.Transforms.Add(spawnRequest.Targets[j]);
            }
        }   
    }
}





