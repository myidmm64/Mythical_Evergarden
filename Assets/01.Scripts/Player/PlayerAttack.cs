using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    bool isCanAttack;

    public IEnumerator StartAttack(Vector2Int myPos, float attackSpeed, int damage, Action SetIdle)
    {
        TestAttack(myPos, damage, SetIdle);
        yield break;

        float time = 0;
        isCanAttack = false;

        while (true)
        {
            if (time == 1)
            {
                SetIdle.Invoke();
                yield break;
            }
            if(isCanAttack)
            {
                for (int i = (int)EDirection.Left; i < (int)EDirection.Down + 1; i++)
                {
                    if (BattleManager.Instance.GetBossUnitOnDice(myPos + Utility.GetDirection((EDirection)i), out BossUnit boss))
                    {
                        boss.GetDamage(damage);
                        yield return null;
                    }
                }
                isCanAttack = false;
            }

            time = Mathf.Clamp(time + Time.deltaTime * attackSpeed, 0, 1);
            yield return null;
        }
    }

    private void TestAttack(Vector2Int myPos, int damage, Action SetIdle)
    {
        var attackRange = DiceManager.Instance.GetCrossDices(myPos, 1); // 십자 
        attackRange.SubDices(myPos);
        foreach (var bossUnit in attackRange.GetIDiceUnits<BossUnit>())
        {
            bossUnit.GetDamage(damage);
        }
        SetIdle.Invoke(); // 테스트용
    }

    public void AttackHit(int isHit)
    {
        isCanAttack = isHit == 1;
    }
}
