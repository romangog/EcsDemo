using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Rigidbody2D Rigidbody => _rigidbody2D;
    public ProjectileHitbox ProjectileHitbox => _projectileHitbox;

    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private ProjectileHitbox _projectileHitbox;
}
