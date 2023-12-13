using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePool : PoolableObject
{
    private ParticleSystem _particleSystem = null;
    private float _timer = 0f;
    private readonly float _minLifeTime = 0.1f;

    public override void PopInit()
    {
        _timer = 0f;
        _particleSystem.Play();
    }

    public override void PushInit()
    {
        _timer = 0f;
    }

    public override void StartInit()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (_particleSystem.particleCount == 0 && _timer >= _minLifeTime)
        {
            //Debug.Log("Push");
            PoolManager.Instance.Push(this);
        }
        _timer += Time.deltaTime;
    }
}