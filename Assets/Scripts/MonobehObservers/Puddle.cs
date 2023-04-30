using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puddle : MonoBehaviour
{
    [SerializeField] private ElementalParticlesComponent _puddlePaticles;
    [SerializeField] private SpriteRendererComponent _spriteRenderer;

    public ElementalParticlesComponent ElementalParticles => _puddlePaticles;
    public SpriteRendererComponent SpriteRenderer => _spriteRenderer;
}
