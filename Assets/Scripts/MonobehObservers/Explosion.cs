using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public SpriteRendererComponent SpriteRenderer => _spriteRenderer;
    public ElementalParticlesComponent ElementalParticles => _elementalParticles;

    [SerializeField] private SpriteRendererComponent _spriteRenderer;
    [SerializeField] private ElementalParticlesComponent _elementalParticles;
}
