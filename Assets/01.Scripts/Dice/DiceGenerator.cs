using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceGenerator
{
    public void GenerateDices(Dictionary<Vector2Int, Dice> dices, DiceGenerateDataSO data, Transform parentTrm, out int maxRow, out int maxColumn)
    {
        string[] rows = data.diceMapStr.Split('\n');
        maxColumn = rows.Length;
        maxRow = rows[0].Length;
        Vector2 startPos = data.diceCenterPosition + GetPaddingPos(-new Vector2(maxRow / 2, maxColumn / 2), data.dicePositionDistance);

        for (int i = 1; i <= maxColumn; i++)
        {
            for (int j = 1; j <= maxRow; j++)
            {
                int number = rows[i - 1][j - 1] - '0';
                Dice dice = PopDice((EDiceType)number);
                if (dice == null) continue;

                Vector2Int diceKey = new Vector2Int(j, i);
                dice.diceKey = diceKey;

                Vector2 dicePosition = startPos + GetPaddingPos(new Vector2(j - 1, maxColumn - i), data.dicePositionDistance);
                dice.transform.position = dicePosition;
                dice.transform.SetParent(parentTrm, false);

                dice.InitDice();
                dices.Add(diceKey, dice);
            }
        }
    }

    private Vector2 GetPaddingPos(Vector2 pos, Vector2 padding)
    {
        return pos * padding;
    }

    private Dice PopDice(EDiceType diceType) => diceType switch
    {
        EDiceType.None => null,
        EDiceType.NormalDice => PoolManager.Instance.Pop(EPoolType.NormalDice) as Dice,
        _ => null
    };
}
