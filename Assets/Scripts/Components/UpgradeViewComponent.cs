using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public struct UpgradeViewComponent
{
    public Image Icon;
    public TMP_Text Description;
    public string EventsEmitterName;
    public WeaponLevel WeaponLevel;
    public GameObject View;
}

public struct ShownTag
{
}

