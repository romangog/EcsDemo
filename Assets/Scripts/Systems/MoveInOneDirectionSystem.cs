using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leopotam.Ecs;

public class MoveInOneDirectionSystem : IEcsRunSystem
{
    EcsFilter<RigidbodyComponent, MoveForwardComponent> _moveForwardFilter;

    public void Run()
    {
        foreach (var id in _moveForwardFilter)
        {
            ref var rigidbody = ref _moveForwardFilter.Get1(id);
            ref var moveForward = ref _moveForwardFilter.Get2(id);
            rigidbody.Rigidbody.MovePosition(rigidbody.Rigidbody.position + moveForward.Direction * moveForward.Speed * Time.deltaTime);
            Debug.Log("MoveForward dir: " + moveForward.Direction);
            Debug.Log("MoveForward speed: " + moveForward.Speed);
        }
    }
}
