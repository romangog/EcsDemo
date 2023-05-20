using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemTrigger : MonoBehaviour
{
    public Action<EntityReference> TriggerEnteredEvent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TriggerEnteredEvent?.Invoke(collision.transform.parent.GetComponent<EntityReference>());
    }
}
