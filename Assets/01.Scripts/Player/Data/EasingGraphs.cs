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

    public static float EaseOutCirc(float x)
    {
        return Mathf.Sqrt(1 - Mathf.Pow(x - 1, 2));
    }

    public static float EaseInOutCirc(float x)
    {
        return x < 0.5
            ? (1 - Mathf.Sqrt(1 - Mathf.Pow(2 * x, 2))) / 2
            : (Mathf.Sqrt(1 - Mathf.Pow(-2 * x + 2, 2)) + 1) / 2;
    }
        
    
    public static float EaseInOutQuint(float x)
    {
        return x < 0.5 ? 16 * x * x * x * x * x : 1 - Mathf.Pow(-2 * x + 2, 5) / 2;
    }


}
