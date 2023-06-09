﻿using System;
using UnityEngine;

[Serializable]
public class GameSettings
{
    public Vector2 SpawnRectSize;
    public float BulletsPathLengthMax;
    public float FragmentsPathLengthMax;
    public float PlayerBaseMoveSpeed;
    public float EnemyBaseMoveSpeed;
    public float LightningReachRadius;
    public float GemCollectionSpeed;
    public float VampirismTimer;
    public Color IceColor;
    public Color BlackColor;
    public LayerMask EnemyColliderLayerMask;
}

[Serializable]
public class WeaponsLevelsSettings
{
    public WeaponLevel.Settings[] AllSettings;
}