using UnityEngine;
using Leopotam.Ecs;

public class FireClosestEnemiesSystem : IEcsRunSystem, IEcsInitSystem
{
    private EcsFilter<EnemyTag, TransformComponent, TargetOnFireComponent> _enemiesFilter;

    private EcsWorld _world;
    private GameSettings _gameSettings;
    private WeaponUpgradeLevels _weaponUpgrades;

    private int _hitsCountTotal;
    private Timer _timer;

    public void Init()
    {
        ResetTimer();
    }

    public void Run()
    {
        _timer.Update();
        if (!_timer.IsOver) return;

        ResetTimer();

        foreach (var enemyId in _enemiesFilter)
        {
            ref var enemyTransform = ref _enemiesFilter.Get2(enemyId);
            ref var enemyOnFire = ref _enemiesFilter.Get3(enemyId);

            RaycastHit2D[] hits = Physics2D.CircleCastAll(
                enemyTransform.Transform.position,
                enemyOnFire.FireCatchRadius,
                Vector3.zero,
                0f,
                _gameSettings.EnemyColliderLayerMask);

            foreach (var hit in hits)
            {
                ref var targetEntity = ref hit.transform.GetComponent<EntityReference>().Entity;

                if (!targetEntity.IsAlive() || targetEntity.Has<TargetOnFireComponent>()) continue;

                // Remove setting on fire in separate system
                targetEntity.Get<CatchFireRequest>();
            }

            _hitsCountTotal += hits.Length - 1;
        }

        _hitsCountTotal = 0;

    }

    private void ResetTimer()
    {
        _timer.Set(0.5f);
    }
}



