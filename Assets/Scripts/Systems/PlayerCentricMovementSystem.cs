using UnityEngine;
using Leopotam.Ecs;

public class PlayerCentricMovementSystem : IEcsRunSystem
{
    EcsWorld _world;
    EcsFilter<EnemyTag, RigidbodyComponent>.Exclude<DeadTag, HitThrowbackTimer> _enemyRigidbodyesFilter;
    EcsFilter<PlayerTag, RigidbodyComponent> _playerPositionFilter;

    public void Run()
    {
        foreach (var enemyId in _enemyRigidbodyesFilter)
        {
            foreach (var playerId in _playerPositionFilter)
            {
                ref var enemyRb = ref _enemyRigidbodyesFilter.Get2(enemyId);
                ref var playerRb = ref _playerPositionFilter.Get2(playerId);

                Vector2 distanceToPlayer = playerRb.Rigidbody.position - enemyRb.Rigidbody.position;

                Vector2 movement = distanceToPlayer.normalized * Time.deltaTime * 3f;
                enemyRb.Rigidbody.MovePosition(enemyRb.Rigidbody.position + movement);
            }
        }
    }
}

