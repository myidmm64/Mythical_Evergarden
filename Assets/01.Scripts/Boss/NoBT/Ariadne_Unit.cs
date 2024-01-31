using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[System.Serializable]
public class RandomPattern
{
    public string name;
    public BossPattern pattern;
    public float weight;
    [HideInInspector]
    public float maxWeight;
}

public class Ariadne_Unit : BossUnit
{
    [SerializeField]
    private List<RandomPattern> _patterns = new List<RandomPattern>();
    private float _maxWeight = 0;

    [SerializeField]
    private PlayerBase _player = null;

    protected override void Start()
    {
        base.Start();
        Init();
        StartPattern();
    }

    private void Init()
    {
        foreach (var pattern in _patterns)
        {
            _maxWeight += pattern.weight;
            pattern.maxWeight = _maxWeight;
        }
    }

    private void StartPattern()
    {

    }

    public BossPattern GetRandomPattern()
    {
        // °¡ÁßÄ¡ ·£´ý
        float random = Random.Range(0f, _maxWeight);
        foreach (var pattern in _patterns)
        {
            if (random <= pattern.maxWeight) 
                return pattern.pattern;
        }
        return null;
    }
}
