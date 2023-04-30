using System;
using UnityEngine;

public struct Timer
{
    public float TimeLeft;
    public bool IsOver;
    private float _setTime;

    public void Update()
    {
        if (IsOver) return;
        TimeLeft = Mathf.MoveTowards(TimeLeft,0f, Time.deltaTime);
        if (TimeLeft == 0f)
            IsOver = true;
    }

    internal void Set(float time)
    {
        TimeLeft = time;
        _setTime = time;
        IsOver = false;
    }

    internal void Reset()
    {
        TimeLeft = _setTime;
        IsOver = false;
    }
}
