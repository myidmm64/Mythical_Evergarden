using UnityEngine;

[System.Serializable]
public class WeightedRandomableObject<T>
{
    public string name;
    public T obj;
    public float weight;
    [HideInInspector]
    public float maxWeight;
}
