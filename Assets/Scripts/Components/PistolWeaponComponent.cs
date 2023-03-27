using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct PistolWeaponComponent
{
    public float ShootingFrequency;
    public float Damage;

}

public struct PistolFiringStatusComponent
{
    public float CurrentReload;
}
