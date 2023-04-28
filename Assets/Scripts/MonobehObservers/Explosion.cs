using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public SpriteRendererComponent SpriteRenderer => _spriteRenderer;

    [SerializeField] private SpriteRendererComponent _spriteRenderer;
}
