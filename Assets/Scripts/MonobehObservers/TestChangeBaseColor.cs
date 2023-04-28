using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TestChangeBaseColor : MonoBehaviour
{
    [SerializeField] private Color _baseColor;
    [SerializeField] private SpriteRenderer _renderer;

    private MaterialPropertyBlock _propertyBlock;

    private void Start()
    {
        _propertyBlock = new MaterialPropertyBlock();
    }

    private void Update()
    {
        _renderer.GetPropertyBlock(_propertyBlock);
        _propertyBlock.SetColor("_BaseColor", _baseColor);
        _renderer.SetPropertyBlock(_propertyBlock);
    }
}
