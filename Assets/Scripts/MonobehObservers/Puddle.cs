using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puddle : MonoBehaviour
{
    [SerializeField] private PuddleParticlesComponent _puddlePaticles;
    [SerializeField] private SpriteRendererComponent _spriteRenderer;

    public PuddleParticlesComponent PuddleParticles => _puddlePaticles;
    public SpriteRendererComponent SpriteRenderer => _spriteRenderer;
}
