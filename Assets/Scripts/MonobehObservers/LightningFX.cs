using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningFX : MonoBehaviour
{
    public LineRendererComponent LineRendererComponent => _lineRendererComponent;

    [SerializeField] private LineRendererComponent _lineRendererComponent;
}
