using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public void StartAttack(Vector2Int myPos, Action callback)
    {
        for(int i = (int)EDirection.Left; i < (int)EDirection.Down + 1; i++)
        {
            if(BattleManager.Instance.GetBossUnitOnDice(myPos + Utility.GetDirection((EDirection)i), out BossUnit boss))
            {
                Debug.Log("Attack");
                boss.GetDamage();
            }
        }
    }
}
