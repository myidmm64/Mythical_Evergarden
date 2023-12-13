using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// 나중에 abstract로 만들 것,
public class Dice : PoolableObject
{
    public IDiceUnit diceUnit = null; // 현재 주사위에 있는 오브젝트
    public bool Moveable => diceUnit != null;

    private int _dicePip = 0; // 주사위 눈
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
