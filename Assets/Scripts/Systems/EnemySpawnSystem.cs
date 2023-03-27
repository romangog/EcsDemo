using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leopotam.Ecs;
using System;

public class EnemySpawnSystem : IEcsRunSystem, IEcsInitSystem
{
    private Prefabs _prefabs;

    private float _spawnTimer;
    private float _spawnTimerMax;

    public void Init()
    {
        _spawnTimerMax = _spawnTimer = 5f;
    }

    public void Run()
    {
        _spawnTimer = Mathf.MoveTowards(_spawnTimer, 0f, Time.deltaTime);
        if (_spawnTimer == 0f)
        {
            _spawnTimer = _spawnTimerMax;
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        GameObject enemy = GameObject.Instantiate(_prefabs.EnemyPrefab);
        enemy.transform.position = new Vector3(UnityEngine.Random.Range(-10f, 10f), UnityEngine.Random.Range(-10f, 10f), 0f);
    }
}
