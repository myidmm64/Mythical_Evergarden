using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestUnit : BossUnit
{
    public override void GetDamage(int damage)
    {
		_bossStat.hp -= damage;
        Debug.Log("TestUnit Hp : " + _bossStat.hp);
    }

    private void Start()
    {
        myPos = new Vector2Int(4, 4);
        if (DiceManager.Instance.TryGetDice(myPos, out Dice dice))
        {
            transform.position = dice.transform.position;
        }

        BattleManager.Instance.AddBossUnit(this);
    }
}
