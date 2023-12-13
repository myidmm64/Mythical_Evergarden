using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingObject : MonoBehaviour
{
    [SerializeField]
    private float _floatRandomDelayMin = 0.2f;
    [SerializeField]
    private float _floatRandomDelayMax = 0.5f;
    [SerializeField]
    private float _floatAmount = 0.5f;

    private void Start()
    {
        float randomDuration = Random.Range(_floatRandomDelayMin, _floatRandomDelayMax);
        transform.DOLocalMoveY(transform.localPosition.y + _floatAmount, randomDuration).SetLoops(-1, LoopType.Yoyo);
    }
}
