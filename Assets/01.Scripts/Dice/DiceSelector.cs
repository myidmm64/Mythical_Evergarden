using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class DiceSelector
{
    private Dictionary<Vector2Int, Dice> _dices = null;
    private Vector2Int _mapSize = Vector2Int.zero;

    public DiceSelector(Dictionary<Vector2Int, Dice> dices, Vector2Int mapSize)
    {
        if (dices == null || mapSize == Vector2Int.zero)
        {
            Debug.LogError("DiceSeletor Constructor Error");
        }
        _dices = dices;
        _mapSize = mapSize;
    }

    public bool TryGetDice(Vector2Int position, out Dice dice)
    {
        return _dices.TryGetValue(position, out dice);
    }

    public IEnumerable<Dice> GetSamePipDices(int dicePip)
    {
        var query = from dice in _dices.Values
                    where dice.dicePip == dicePip
                    select dice;
        return query;
    }

    public IEnumerable<Dice> GetDiceRow(int rowNum)
    {
        var query = from diceKeyValue in _dices
                    where diceKeyValue.Key.y == rowNum
                    select diceKeyValue.Value;
        return query;
    }

    public IEnumerable<Dice> GetDiceColumn(int columnNum)
    {
        var query = from diceKeyValue in _dices
                    where diceKeyValue.Key.x == columnNum
                    select diceKeyValue.Value;
        return query;
    }

    public IEnumerable<Dice> GetDiceLine(Vector2Int startPos, EDirection direction, int count, bool plusReflect = false, EDirection rotateDirection = EDirection.Up)
    {
        if (count == -1)
        {
            count = Utility.GetMaxCountWithDirection(direction, _mapSize);
        }
        List<Dice> result = new List<Dice>();
        Vector2Int dir = Utility.GetDirection(direction);
        Vector2Int reflectDir = Utility.GetDirection(Utility.GetReflectDirection(direction));
        for (int i = 0; i <= count; i++)
        {
            Vector2Int diceKey = startPos + dir * i;
            if (TryGetDice(GetRotatedDiceKey(diceKey, startPos, rotateDirection), out Dice dice))
            {
                result.Add(dice);
            }
            if (plusReflect)
            {
                Vector2Int reflectDiceKey = startPos + reflectDir * i;
                if (TryGetDice(GetRotatedDiceKey(reflectDiceKey, startPos, rotateDirection), out Dice reflectDice))
                {
                    result.Add(reflectDice);
                }
            }
        }
        return result.ExcludeReduplication();
    }

    public IEnumerable<Dice> GetCrossDices(Vector2Int startPos, int count)
    {
        List<Dice> result = new List<Dice>();
        result.AddRange(GetDiceLine(startPos, EDirection.Up, count, true));
        result.AddRange(GetDiceLine(startPos, EDirection.Right, count, true));
        return result.ExcludeReduplication();
    }

    public IEnumerable<Dice> GetXCrossDices(Vector2Int startPos, int count)
    {
        List<Dice> result = new List<Dice>();
        result.AddRange(GetDiceLine(startPos, EDirection.LeftUp, count, true));
        result.AddRange(GetDiceLine(startPos, EDirection.RightUp, count, true));
        return result.ExcludeReduplication();
    }

    public IEnumerable<Dice> GetDiceSquare(Vector2Int centerPos, int size, bool isBorder = false)
    {
        if (size % 2 == 0)
        {
            Debug.LogWarning("size가 짝수입니다. 홀수로 변환합니다.");
            size += 1;
        }
        return GetDiceRectangle(centerPos, size, size, isBorder);
    }

    public IEnumerable<Dice> GetDiceRotatedSquare(Vector2Int centerPos, int centerDistance, bool isBorder = false)
    {
        List<Dice> result = new List<Dice>();
        int curDistance = centerDistance;
        if (!isBorder)
        {
            if (TryGetDice(centerPos, out Dice dice))
            {
                result.Add(dice);
            }
        }
        while (curDistance > 0)
        {
            Vector2Int leftPos = new Vector2Int(centerPos.x - curDistance, centerPos.y);
            Vector2Int rightPos = new Vector2Int(centerPos.x + curDistance, centerPos.y);
            result.AddRange(GetDiceLine(leftPos, EDirection.RightUp, curDistance));
            result.AddRange(GetDiceLine(leftPos, EDirection.RightDown, curDistance));
            result.AddRange(GetDiceLine(rightPos, EDirection.LeftUp, curDistance));
            result.AddRange(GetDiceLine(rightPos, EDirection.LeftDown, curDistance));
            if (isBorder) break;
            curDistance--;
        }
        return result.ExcludeReduplication();
    }

    public IEnumerable<Dice> GetDiceRectangle(Vector2Int centerPos, int width, int height, bool isBorder = false, EDirection rotateDirection = EDirection.Up)
    {
        if (width == -1)
        {
            width = Utility.GetMaxCountWithDirection(EDirection.Right, _mapSize);
        }
        else if (width % 2 == 0)
        {
            Debug.LogWarning("width가 짝수입니다. 홀수로 변환합니다.");
            width += 1;
        }
        if (height == -1)
        {
            height = Utility.GetMaxCountWithDirection(EDirection.Up, _mapSize);
        }
        else if (height % 2 == 0)
        {
            Debug.LogWarning("height가 짝수입니다. 홀수로 변환합니다.");
            height += 1;
        }
        List<Dice> result = new List<Dice>();
        Vector2Int searchStartPos = new Vector2Int(-(width / 2), -(height / 2));
        Vector2Int searchEndPos = new Vector2Int(width / 2, height / 2);
        Vector2Int position = Vector2Int.zero;
        for (int x = searchStartPos.x; x <= searchEndPos.x; x++)
        {
            for (int y = searchStartPos.y; y <= searchEndPos.y; y++)
            {
                position.x = x;
                position.y = y;
                if (TryGetDice(GetRotatedDiceKey(centerPos + position, centerPos, rotateDirection), out Dice dice))
                {
                    result.Add(dice);
                }
            }
        }

        if (isBorder)
        {
            return result.ExceptDices(GetDiceRectangle(centerPos, width - 2, height - 2, false, rotateDirection));
        }
        return result;
    }

    public IEnumerable<Dice> GetDicesWithPattern(Vector2Int centerPos, string pattern, EDirection rotateDirection = EDirection.Up)
    {
        List<Dice> result = new List<Dice>();
        List<Vector2Int> diceKeys = GetStringToDiceKeys(centerPos, pattern);
        foreach (var diceKey in diceKeys)
        {
            if (TryGetDice(GetRotatedDiceKey(diceKey, centerPos, rotateDirection), out Dice dice))
            {
                result.Add(dice);
            }
        }
        return result;
    }

    private Vector2Int GetRotatedDiceKey(Vector2Int targetKey, Vector2Int startkey, EDirection rotateDirection)
    {
        if (rotateDirection == EDirection.Up) return targetKey;
        Vector2 result = Quaternion.AngleAxis(Utility.GetZRotate(rotateDirection), Vector3.forward) * ((Vector2)(targetKey - startkey));
        return startkey + Vector2Int.RoundToInt(result);
    }

    /// <summary>
    /// string을 diceKey 집합으로 변환합니다
    /// </summary>
    /// <param name="targetString"></param>
    /// <returns></returns>
    public List<Vector2Int> GetStringToDiceKeys(Vector2Int centerPos, string targetString)
    {
        List<Vector2Int> result = new List<Vector2Int>();
        string[] rows = targetString.Split('\n');
        int maxColumn = rows.Length;
        int maxRow = rows[0].Length;
        Vector2Int startPos = centerPos + new Vector2Int(-(maxRow / 2), -(maxColumn / 2));

        for (int y = 1; y <= maxColumn; y++)
        {
            for (int x = 1; x <= maxRow; x++)
            {
                int number = rows[y - 1][x - 1] - '0';
                if (number == 0) continue;

                Vector2Int diceKey = startPos + new Vector2Int(x - 1, maxColumn - y);
                result.Add(diceKey);
            }
        }

        return result;
    }
}
