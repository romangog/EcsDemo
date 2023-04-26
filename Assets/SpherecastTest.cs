using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpherecastTest : MonoBehaviour
{
    private static int hitsCountTotal;
    private Timer _timer;

    [SerializeField] private LayerMask _enemyColliderLayerMask;
    private void Start()
    {
        ResetTimer();
    }
    // Update is called once per frame
    void Update()
    {
        _timer.Update();
        if (_timer.IsOver)
            SphereCast();
    }

    private void SphereCast()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(this.transform.position, 10f, Vector3.up, 1f, _enemyColliderLayerMask);
        hitsCountTotal += hits.Length;
    }

    private void LateUpdate()
    {
        if (_timer.IsOver)
        {
            Debug.Log("HitsTotal: " + hitsCountTotal);
            ResetTimer();
            hitsCountTotal = 0;
        }
    }

    private void ResetTimer()
    {
        _timer.Set(1f);
    }
}
