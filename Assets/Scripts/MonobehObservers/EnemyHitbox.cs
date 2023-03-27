using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leopotam.Ecs;

public class EnemyHitbox : MonoBehaviour
{
    [SerializeField] private EntityReference _entityReference;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.TryGetComponent(out ProjectileHitbox hitbox))
        {
            hitbox.PassHitEntity(_entityReference); 
        }
    }

    internal void PassHitPlayerEntity(EntityReference entityReference)
    {
        // надо передать в систему ивент о том, что враг задел игрока
        // Для этого нужен еще и урон врага
        // Урон врага привязан к его Entity

        entityReference.Entity.Get<AccumulativeDamageComponent>().Damage += _entityReference.Entity.Get<DamageComponent>().Damage;
    }
}


