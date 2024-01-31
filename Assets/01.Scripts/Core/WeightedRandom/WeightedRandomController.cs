using System.Collections.Generic;
using UnityEngine;

public class WeightedRandomController<T>
{
    private float _maxWeight = 0f;
    private List<WeightedRandomableObject<T>> _items = new List<WeightedRandomableObject<T>>();

    public WeightedRandomController(List<WeightedRandomableObject<T>> items)
    {
        _items = items;
        foreach (var item in _items)
        {
            _maxWeight += item.weight;
            item.maxWeight = _maxWeight;
        }
    }

    public T GetWeightedRandomObj()
    {
        // °¡ÁßÄ¡ ·£´ý
        float random = Random.Range(0f, _maxWeight);
        foreach (var item in _items)
        {
            if (random <= item.maxWeight)
                return item.obj;
        }
        return default(T);
    }
}