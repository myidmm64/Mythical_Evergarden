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
    private Animator _animator = null;
    [SerializeField]
    private TextMeshPro _text = null;

    private void Start()
    {
        dicePip = Random.Range(1, 7);
        transform.GetComponentInChildren<TextMeshPro>().SetText(dicePip.ToString());
    }

    public void InitDice()
    {

    }

    public void RollDiceWithRandom(int min, int max)
    {
        int random = Random.Range(min, max);
        dicePip = random;

        //_animator.transform.DOPunchPosition(Vector2.one * 0.1f, 0.3f);
        _animator.Play("RollingDice");
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
