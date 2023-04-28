using System;
using UnityEngine;

[Serializable]
public class GameSettings
{
    public Vector2 SpawnRectSize;
    public float BulletsPathLengthMax;
    public float FragmentsPathLengthMax;
    public float PlayerBaseMoveSpeed;
    public float EnemyBaseMoveSpeed;
    public Color IceColor;
    public Color BlackColor;
    public LayerMask EnemyColliderLayerMask;
}