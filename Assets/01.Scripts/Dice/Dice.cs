using System.Collections;
using System.Collections.Generic;
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
    public int dicePip { get => _dicePip; set { _dicePip = value; RollAnimation(); } }
    public Vector2Int diceKey = Vector2Int.zero;

    private void Start()
    {
        dicePip = 1;
    }

    public void InitDice()
    {

    }

    public void RollDiceWithRandom(int min, int max)
    {
        int random = Random.Range(min, max);
        dicePip = random;
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
