using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    public SpriteRendererComponent SpriteRenderer => _spriteRenderer;
    public GemTrigger GemTrigger => _gemTrigger;

    [SerializeField] private SpriteRendererComponent _spriteRenderer;
    [SerializeField] private GemTrigger _gemTrigger;

}
