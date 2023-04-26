using System;
using UnityEngine;

[Serializable]
public class GameSettings
{
    public Vector2 SpawnRectSize;
    public float BulletsPathLengthMax;
    public float FragmentsPathLengthMax;
    public LayerMask EnemyColliderLayerMask;
}