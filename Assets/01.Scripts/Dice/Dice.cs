using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using BehaviorDesigner.Runtime;

[System.Serializable]
public class SharedDice : SharedVariable<Dice>
{
    public static implicit operator SharedDice(Dice value) { return new SharedDice { Value = value }; }
}

// ���߿� abstract�� ���� ��,
[System.Serializable]
public class Dice : PoolableObject
{
    public IDiceUnit diceUnit = null; // ���� �ֻ����� �ִ� ������Ʈ
    public bool Moveable => diceUnit != null;

    private int _dicePip = 0; // �ֻ��� ��
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
    private TextMeshPro _text = null;
    [SerializeField]
    private Color _redColor = Color.red;
    [SerializeField]
    private SpriteRenderer _fillRenerer = null;

    private void Start()
    {
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

    public void ColorAnimation(float colorDuration = 0.2f)
    {
        _fillRenerer.DOColor(_redColor, colorDuration).SetLoops(2, LoopType.Yoyo);
    }

    public void RollAnimation(float pastPip, float endPip)
    {
        transform.DOPunchPosition(Vector2.up * 0.1f, 0.3f);
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
