using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leopotam.Ecs;

public class SetImageFillingSystem : IEcsRunSystem
{
    EcsFilter<PlayerTag, ImageComponent,  HealthComponent> _playerHealthFilter;

    public void Run()
    {
        foreach (var playerId in _playerHealthFilter)
        {
            ref var image = ref _playerHealthFilter.Get2(playerId);
            ref var health = ref _playerHealthFilter.Get3(playerId);

            image.Image.fillAmount = health.CurrentHealth / health.MaxHealth;
        }

    }
}
