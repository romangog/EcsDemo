using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leopotam.Ecs;

public class CenterCameraOnPlayerSystem : IEcsRunSystem
{
    EcsFilter<PlayerTag, RigidbodyComponent> _playerRigidbodyFilter;

    public void Run()
    {
        foreach (var i in _playerRigidbodyFilter)
        {
            ref var playerRigidbody = ref _playerRigidbodyFilter.Get2(i);

            
        }
    }
}
