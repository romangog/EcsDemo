using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leopotam.Ecs;

public class HealthbarSystem : IEcsRunSystem
{
    EcsFilter<EntityReferenceComponent, HealthbarUIComponent> _healthUIFilter;

    public void Run()
    {
        foreach (var id in _healthUIFilter)
        {
            ref var rootEntity = ref _healthUIFilter.Get1(id).RootEntity;
            ref var healthbar = ref _healthUIFilter.Get2(id);

            ref var healthComp = ref rootEntity.Get<HealthComponent>();
            healthbar.Image.fillAmount = healthComp.CurrentHealth / healthComp.MaxHealth;
        }    
    }
}
