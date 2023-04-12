using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitbox : MonoBehaviour
{
    [SerializeField] private EntityReference _entityReference;


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!enabled) return;
        if (collision.collider.TryGetComponent(out EnemyHitbox enemyHitbox))
        {
            enemyHitbox.PassHitPlayerEntity(_entityReference);
        }
    }
}
