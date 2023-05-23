using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leopotam.Ecs;

public class PlayerInputMovementSystem : IEcsRunSystem
{
    EcsFilter<PlayerTag, RigidbodyComponent>.Exclude<DeadTag> _playerRigidbodiesFilter;

    private GameSettings _gameSetting;

    public void Run()
    {
        foreach (var playerId in _playerRigidbodiesFilter)
        {
            ref var rb = ref _playerRigidbodiesFilter.Get2(playerId);

            Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
            Vector2 movement = input * _gameSetting.PlayerBaseMoveSpeed * Time.fixedDeltaTime;
            rb.Rigidbody.MovePosition(rb.Rigidbody.position +  movement);
        }
    }
}
