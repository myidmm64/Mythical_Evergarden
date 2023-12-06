using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class IDiceUnitExtensionMethod
{
    public static bool ChangeMyDice(this IDiceUnit unit, Vector2Int targetPos)
    {
        if (DiceManager.Instance == null) return false;

        if (DiceManager.Instance.TryGetDice(targetPos, out Dice dice))
        {
            if (unit.myDice != null)
            {
                unit.ExitDice();
                unit.myDice.diceUnit = null;
            }

            unit.myDice = dice;
            dice.diceUnit = unit;
            unit.myPos = targetPos;
            unit.EnterDice();
            return true;
        }
        return false;
    }

    public static void ClearMyDice(this IDiceUnit unit)
    {
        if (unit.myDice != null)
        {
            unit.ExitDice();
            unit.myDice.diceUnit = null;
        }
    }
}