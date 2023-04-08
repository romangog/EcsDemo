using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leopotam.Ecs;
using System;

public class EnemySpawnSystem : IEcsRunSystem, IEcsInitSystem
{
    private Prefabs _prefabs;
    private GameSettings _gameSettings;

    private float _spawnTimer;
    private float _spawnTimerMax;
    private float _gameTimer;
    private int _enemiesCount;

    private EcsFilter<PlayerTag, TransformComponent> _playerTransformFilter;
    private EcsFilter<MainCameraTag, CameraComponent> _mainCameraFilter;
    private EcsFilter<EnemyTag>.Exclude<DeadTag> _aliveEnemiesFilter;

    public void Init()
    {
        _spawnTimerMax = _spawnTimer = 5f;
    }

    public void Run()
    {
        _spawnTimer = Mathf.MoveTowards(_spawnTimer, 0f, Time.deltaTime);

        if (_spawnTimer == 0f)
        {
            if (_aliveEnemiesFilter.GetEntitiesCount() >= 10)
            {
                return;
            }

            ref var playerTransform = ref _playerTransformFilter.Get2(0);
            ref var mainCamera = ref _mainCameraFilter.Get2(0);


            _spawnTimerMax = Mathf.Max(0.5f, _spawnTimerMax * 0.975f);
            _spawnTimer = _spawnTimerMax;

            Vector2 spawnPosition;
            Vector2 spawnRectHalf = new Vector2(mainCamera.Camera.orthographicSize * (Screen.width / (float)Screen.height), mainCamera.Camera.orthographicSize);
            Vector2 playerPos = playerTransform.Transform.position;
            bool IsVertical = UnityEngine.Random.Range(0, 2) == 1;
            float sideMod = (UnityEngine.Random.Range(0, 2) == 1) ? 1 : -1;
            if (IsVertical)
            {
                // up-buttom
                float randomX = playerPos.x + UnityEngine.Random.Range(-spawnRectHalf.x, spawnRectHalf.x);
                float heightSide = playerPos.y + spawnRectHalf.y * sideMod;
                spawnPosition = new Vector2(randomX, heightSide);
            }
            else
            {
                // left-right
                float xSide = playerPos.x + spawnRectHalf.x * sideMod;
                float randomHeight = playerPos.y + UnityEngine.Random.Range(-spawnRectHalf.y, spawnRectHalf.y);
                spawnPosition = new Vector2(xSide, randomHeight);
            }

            SpawnEnemy(spawnPosition);

        }
    }

    private void SpawnEnemy(Vector2 position)
    {
        GameObject enemy = GameObject.Instantiate(_prefabs.EnemyPrefab, position, Quaternion.identity, null);
    }
}
