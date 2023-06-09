using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct HealthComponent
{
    public float CurrentHealth;
    public float MaxHealth;

    public void Reinitialize()
    {
        CurrentHealth = 100;
        MaxHealth = 100;
    }
}
