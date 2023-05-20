using System.Collections.Generic;
using UnityEngine;

public class MonoComponentPool<T> : IObjectPool<T> where T : MonoBehaviour
{
    private List<T> _pool;
    private T _prefab;
    private int _nextObjectIndex;
    private Transform _parent;

    public MonoComponentPool(T prefab, int initialCapacity, Transform parent)
    {
        _prefab = prefab;
        _pool = new List<T>(initialCapacity);
        _parent = parent;

        for (int i = 0; i < initialCapacity; i++)
        {
            ExpandPool();
        }

        foreach (var obj in _pool)
        {
            obj.gameObject.SetActive(false);
        }
    }

    public T Get()
    {
        for (int i = 0; i < _pool.Count; i++)
        {
            if (!_pool[i].gameObject.activeSelf)
            {
                _pool[i].gameObject.SetActive(true);
                return _pool[i];
            }
        }
        ExpandPool();
        return _pool[_pool.Count - 1];
    }

    public void ExpandPool()
    {
        _pool.Add(Object.Instantiate(_prefab, _parent));
    }

    public void Return(GameObject obj)
    {
        obj.SetActive(false);
    }
}
