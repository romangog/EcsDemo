using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class Extentions
{
    public static void SetAlpha(this Graphic graphic, float alpha)
    {
        graphic.color = new Color(graphic.color.r, graphic.color.g, graphic.color.b, alpha);
    }

    public static Vector2 SetX(this Vector2 vector, float x)
    {
        return new Vector2(x, vector.y);
    }

    public static Vector2 SetY(this Vector2 vector, float y)
    {
        return new Vector2(vector.x, y);
    }

    public static Vector3 SetX(this Vector3 vector, float x)
    {
        return new Vector3(x, vector.y, vector.z);
    }

    public static Vector3 SetY(this Vector3 vector, float y)
    {
        return new Vector3(vector.x, y, vector.z);
    }

    public static Vector3 SetZ(this Vector3 vector, float z)
    {
        return new Vector3(vector.x, vector.y, z);
    }

    public static Vector2 TransformAngle180(this Vector2 v)
    {
        if (v.x > 180f) v.x -= 360f;
        if (v.y > 180f) v.y -= 360f;
        return v;
    }

    public static Vector2 TransformAngle180(this Vector3 v)
    {
        if (v.x > 180f) v.x -= 360f;
        if (v.y > 180f) v.y -= 360f;
        return v;
    }

    public static Vector2 ClampLocalAngle(this Vector2 v, Vector2 min, Vector2 max)
    {
        v = Vector2.Min(v, max);
        v = Vector2.Max(v, min);
        return v;
    }

    public static Vector2 TransformAngle360(this Vector2 v)
    {
        if (v.x < 0f) v.x += 360f;
        if (v.y < 0f) v.y += 360f;
        return v;
    }

    public static Vector2 Global2Relative(this Vector2 v, Transform relative)
    {
        v = v.TransformAngle180();
        v -= (Vector2)relative.eulerAngles.TransformAngle180();
        v = v.TransformAngle180();
        v.x = -v.x;
        return v;
    }

    public static Vector2 Relative2Global(this Vector2 v, Transform relative)
    {
        v.x = -v.x;
        v = v.TransformAngle360();
        v += (Vector2)relative.eulerAngles.TransformAngle180();
        v = v.TransformAngle360();

        return v;
    }

    public static Vector2 GetGlobalClampedEulers(Vector2 eulers, Transform parent, Vector2 min, Vector2 max)
    {
        Vector2 playerViewGlobalEulers = eulers;
        Vector2 relative = playerViewGlobalEulers.Global2Relative(parent);
        Vector2 clamped = relative.ClampLocalAngle(min, max);
        Vector2 globalClamped = clamped.Relative2Global(parent);
        return globalClamped;
    }
}