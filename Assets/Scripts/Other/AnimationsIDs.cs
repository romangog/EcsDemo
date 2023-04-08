using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationsIDs
{
    public static int Die;

    internal static void Initialize()
    {
        Die = Animator.StringToHash("Die");
    }
}
