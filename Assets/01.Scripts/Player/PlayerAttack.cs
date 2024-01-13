using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    bool isCanAttack;

    public IEnumerator StartAttack(Vector2Int myPos, float attackSpeed, int damage, Action SetIdle)
    {
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
                foreach(var bossUnit in GetBossUnitsInAttackRange(myPos))
                {
                    bossUnit.GetDamage(damage);
                    yield return null;
                }
                isCanAttack = false;
            }

            time = Mathf.Clamp(time + Time.deltaTime * attackSpeed, 0, 1);
            yield return null;
        }
    }

    private IEnumerable<BossUnit> GetBossUnitsInAttackRange(Vector2Int myPos)
    {
        var attackRange = DiceManager.Instance.GetCrossDices(myPos, 1); // 십자 
        attackRange = attackRange.SubDices(myPos); // 자신 위치 뺌
        return attackRange.GetIDiceUnits<BossUnit>();
    }

    public void AttackHit(int isHit)
    {
        isCanAttack = isHit == 1;
    }
}
