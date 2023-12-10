using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class IDiceUnitExtensionMethod
{
    /// <summary>
    /// targetPos에 Dice가 있다면 코드 상으로 이동시킵니다.
    /// </summary>
    /// <param name="unit"></param>
    /// <param name="targetPos"></param>
    /// <returns></returns>
    public static bool ChangeMyDice(this IDiceUnit unit, Vector2Int targetPos)
    {
        if (DiceManager.Instance == null) return false;

        if (DiceManager.Instance.TryGetDice(targetPos, out Dice dice))
        {
            if (unit.myDice != null)
            {
                unit.myDice.diceUnit = null;
                unit.ExitDice(unit.myDice);
            }

            dice.diceUnit = unit;
            unit.myPos = targetPos;
            unit.myDice = dice;
            unit.EnterDice(dice);
            return true;
        }
        return false;
    }

    /// <summary>
    /// myDice가 있으면 코드 상으로 해제시킵니다.
    /// </summary>
    /// <param name="unit"></param>
    public static void ClearMyDice(this IDiceUnit unit)
    {
        if (unit.myDice != null)
        {
            unit.myDice.diceUnit = null;
            unit.ExitDice(unit.myDice);
        }
    }
}
