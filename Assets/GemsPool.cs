using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GemsPool : MonoBehaviour, IObjectPool<Gem>
{
    [SerializeField] private int _initialSize;
    private MonoComponentPool<Gem> _gameObjectsPool;

    private Prefabs _prefabs;

    [Inject]
    private void Construct(Prefabs prefabs)
    {
        _prefabs = prefabs;
        _gameObjectsPool = new MonoComponentPool<Gem>(_prefabs.GemPrefab, _initialSize, this.transform);
    }

    public Gem Get()
    {
        Gem gem = _gameObjectsPool.Get();
        return gem;
    }

    public void Return(GameObject obj)
    {
        _gameObjectsPool.Return(obj);
    }
}
