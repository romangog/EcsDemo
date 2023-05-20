using UnityEngine;
using Leopotam.Ecs;

public class PlayerCentricMovementSystem : IEcsRunSystem
{
    private EcsFilter<EnemyTag, RigidbodyComponent, SpeedMultiplierComponent>.Exclude<DeadTag, HitThrowbackTimer> _enemyRigidbodyesFilter;
    private EcsFilter<PlayerTag, RigidbodyComponent> _playerPositionFilter;

    private EcsWorld _world;
    private GameSettings _gameSettings;
    public void Run()
    {
        foreach (var enemyId in _enemyRigidbodyesFilter)
        {
            ref var enemyRb = ref _enemyRigidbodyesFilter.Get2(enemyId);
            ref var enemySpeedMod = ref _enemyRigidbodyesFilter.Get3(enemyId);

            foreach (var playerId in _playerPositionFilter)
            {
                ref var playerRb = ref _playerPositionFilter.Get2(playerId);

                Vector2 distanceToPlayer = playerRb.Rigidbody.position - enemyRb.Rigidbody.position;

                Vector2 movement = distanceToPlayer.normalized * Time.deltaTime * _gameSettings.EnemyBaseMoveSpeed * enemySpeedMod.Multiplier;
                enemyRb.Rigidbody.MovePosition(enemyRb.Rigidbody.position + movement);
            }
            enemySpeedMod.Multiplier = 1f;
        }
    }
}



