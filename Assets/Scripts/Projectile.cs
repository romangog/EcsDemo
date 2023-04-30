using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Rigidbody2D Rigidbody => _rigidbody2D;
    public ProjectileHitbox ProjectileHitbox => _projectileHitbox;
    public SpriteRendererComponent SpriteRenderer => _spriteRenderer;
    public ElementalParticlesComponent ElementalParticles => _elementalParticles;

    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private ProjectileHitbox _projectileHitbox;
    [SerializeField] private SpriteRendererComponent _spriteRenderer;
    [SerializeField] private ElementalParticlesComponent _elementalParticles;
}
