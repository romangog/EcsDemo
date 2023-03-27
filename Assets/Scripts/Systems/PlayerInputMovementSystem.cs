using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leopotam.Ecs;

public class PlayerInputMovementSystem : IEcsRunSystem
{
    EcsFilter<PlayerTag, RigidbodyComponent> _playerRigidbodiesFilter;
    public void Run()
    {
        foreach (var playerId in _playerRigidbodiesFilter)
        {
            ref var rb = ref _playerRigidbodiesFilter.Get2(playerId);

            Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            Vector2 movement = input * 4f * Time.deltaTime;
            rb.Rigidbody.MovePosition(rb.Rigidbody.position +  movement);
        }
    }
}
