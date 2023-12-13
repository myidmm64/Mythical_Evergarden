using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// ���߿� abstract�� ���� ��,
public class Dice : PoolableObject
{
    public IDiceUnit diceUnit = null; // ���� �ֻ����� �ִ� ������Ʈ
    public bool Moveable => diceUnit != null;

    private int _dicePip = 0; // �ֻ��� ��
    public int dicePip { get => _dicePip; set { _dicePip = value; RollAnimation(); } }
    public Vector2Int diceKey = Vector2Int.zero;

    [SerializeField]
    private TextMeshPro _text = null;

    private void Start()
    {
        dicePip = Random.Range(1, 7);
        _text.SetText(dicePip.ToString());
    }

    public void InitDice()
    {

    }

    public void RollDiceWithRandom(int min, int max)
    {
        int random = Random.Range(min, max);
        float pastPip = dicePip;
        dicePip = random;

        transform.DOPunchPosition(Vector2.up * 0.1f, 0.3f);
        DOTween.To(() => pastPip, x => { _text.SetText(x.ToString("N0")); }, dicePip, 0.2f);
        _text.SetText(dicePip.ToString());
    }

    public void RollAnimation()
    {
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
