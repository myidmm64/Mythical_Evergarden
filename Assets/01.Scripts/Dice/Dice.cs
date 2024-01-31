using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

// 나중에 abstract로 만들 것,
public class Dice : PoolableObject
{
    public IDiceUnit diceUnit = null; // 현재 주사위에 있는 오브젝트
    public bool Moveable => diceUnit != null;

    private int _dicePip = 0; // 주사위 눈
    public int dicePip
    {
        get => _dicePip;
        set
        {
            RollAnimation(_dicePip, value);
            _dicePip = value;
        }
    }
    public Vector2Int diceKey = Vector2Int.zero;

    [SerializeField]
    private Transform _spriteTransform = null;
    [SerializeField]
    private TextMeshPro _text = null;
    [SerializeField]
    private Color _redColor = Color.red;
    private Color _originColor = Color.white;
    [SerializeField]
    private SpriteRenderer _fillRenerer = null;
    private Vector3 _originPos = Vector3.zero;

    private void Start()
    {
        _originColor = _fillRenerer.color;
        _originPos = _spriteTransform.position;
        dicePip = Random.Range(1, 7);
    }

    public void InitDice()
    {

    }

    public void RollDiceWithRandom(int min, int max)
    {
        int random = Random.Range(min, max);
        dicePip = random;
    }

    public void ColorAnimation(float colorDuration = 0.2f, Action callback = null)
    {
        _fillRenerer.DOKill();
        _fillRenerer.color = _originColor;
        _fillRenerer.DOColor(_redColor, colorDuration).SetLoops(2, LoopType.Yoyo).OnComplete(() => callback?.Invoke()).SetEase(Ease.Linear);
    }

    public void RollAnimation(float pastPip, float endPip)
    {
        _spriteTransform.DOKill();
        _spriteTransform.position = _originPos;
        _spriteTransform.DOPunchPosition(Vector2.up * 0.1f, 0.3f);
        DOTween.To(() => pastPip, x => { _text.SetText(x.ToString("N0")); }, endPip, 0.2f)
            .OnComplete(() =>
            {
                _text.SetText(endPip.ToString());
            });
        GameObject obj = PoolManager.Instance.Pop(EPoolType.DiceEffect).gameObject;
        obj.transform.position = transform.position;
    }

    public override void PopInit()
    {
    }

    public override void PushInit()
    {
    }

    public override void StartInit()
    {
    }
}
