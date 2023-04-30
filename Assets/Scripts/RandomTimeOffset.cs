using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTimeOffset : MonoBehaviour
{
    [SerializeField] private LineRenderer _renderer;
    private MaterialPropertyBlock _propertyBlock;
    
    void Start()
    {
        _propertyBlock = new MaterialPropertyBlock();
        _renderer.GetPropertyBlock(_propertyBlock);
        _propertyBlock.SetFloat("_TimeOffset", UnityEngine.Random.Range(0f,1f));
        _renderer.SetPropertyBlock(_propertyBlock);
    }
}
