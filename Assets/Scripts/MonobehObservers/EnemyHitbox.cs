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
        if (!enabled) return;
        if (other.TryGetComponent(out ProjectileHitbox hitbox))
        {
            hitbox.PassHitEntity(_entityReference);
        }
    }

    internal void PassHitPlayerEntity(EntityReference entityReference)
    {
        // ���� �������� � ������� ����� � ���, ��� ���� ����� ������
        // ��� ����� ����� ��� � ���� �����
        // ���� ����� �������� � ��� Entity
        if (!entityReference.Entity.IsAlive() || !_entityReference.Entity.IsAlive()) return;
        entityReference.Entity.Get<AccumulativeDamageComponent>().Damage += _entityReference.Entity.Get<DamageComponent>().Damage;
    }
}


