using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 나중에 abstract로 만들 것,
public class Dice : PoolableObject
{
    public IDiceUnit diceUnit = null; // 현재 주사위에 있는 오브젝트
    public bool Moveable => diceUnit != null;

    public Vector2 diceKey = Vector2.zero;
    private int _dicePip = 0; // 주사위 눈
    public int dicePip { get => _dicePip; set { _dicePip = value; RollAnimation(); } }

    private void Start()
    {
        dicePip = 1;
    }

    public void RollDiceWithRandom(int min, int max)
    {
        int random = Random.Range(min, max);
        dicePip = random;
    }

    public void RollAnimation()
    {
        Debug.Log("Roll");
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
