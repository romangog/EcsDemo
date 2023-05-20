using System.Collections;
using UnityEngine;
using Zenject;

public class ProjectilesPool : MonoBehaviour, IObjectPool<Projectile>
{
    [SerializeField] private int _initialSize;
    private MonoComponentPool<Projectile> _gameObjectsPool;

    private Prefabs _prefabs;

    [Inject]
    private void Construct(Prefabs prefabs)
    {
        _prefabs = prefabs;
        _gameObjectsPool = new MonoComponentPool<Projectile>(_prefabs.ProjectilePrefab, _initialSize, this.transform);
    }

    public Projectile Get()
    {
        Projectile projectile = _gameObjectsPool.Get();
        return projectile;
    }

    public void Return(GameObject obj)
    {
        _gameObjectsPool.Return(obj);
    }
}


public interface IObjectPool<T> where T : Object
{
    T Get();
    void Return(GameObject obj);
}
