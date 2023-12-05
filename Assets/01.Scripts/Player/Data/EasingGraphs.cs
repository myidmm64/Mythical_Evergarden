using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EasingGraphs 
{
    public static float EaseInSin(float x)
    {
        return 1 - Mathf.Cos((x * Mathf.PI) / 2);
    }

    public static float EaseOutSin(float x)
    {
        return Mathf.Sin((x * Mathf.PI) / 2);
    }
}
