using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public IEnumerable<Dice> GetDiceLine(Vector2Int startPos, EDirection direction, bool plusReflect)
    {
        List<Dice> result = new List<Dice>();
        int maxCount = Utility.GetMaxCountWithDirection(direction, _mapSize);
        result.AddRange(GetDiceLine(startPos, direction, maxCount, plusReflect));
        return result;
    }

    public IEnumerable<Dice> GetDiceLine(Vector2Int startPos, EDirection direction, int count, bool plusReflect)
    {
        List<Dice> result = new List<Dice>();
        Vector2Int dir = Utility.GetDirection(direction);
        Vector2Int reflectDir = Utility.GetDirection(Utility.GetReflectDirection(direction));
        for (int i = 0; i <= count; i++)
        {
            if (TryGetDice(startPos + dir * i, out Dice dice))
            {
                result.Add(dice);
            }
            if (plusReflect)
            {
                if (TryGetDice(startPos + reflectDir * i, out Dice reflectDice))
                {
                    result.Add(reflectDice);
                }
            }
        }
        return result.ExcludeReduplication();
    }

    public IEnumerable<Dice> GetCrossDices(Vector2Int startPos, int count)
    {
        bool isAll = count == -1;
        List<Dice> result = new List<Dice>();
        if (isAll)
        {
            result.AddRange(GetDiceLine(startPos, EDirection.Up, true));
            result.AddRange(GetDiceLine(startPos, EDirection.Right, true));
        }
        else
        {
            result.AddRange(GetDiceLine(startPos, EDirection.Up, count, true));
            result.AddRange(GetDiceLine(startPos, EDirection.Right, count, true));
        }
        return result.ExcludeReduplication();
    }

    public IEnumerable<Dice> GetXCrossDices(Vector2Int startPos, int count)
    {
        bool isAll = count == -1;
        List<Dice> result = new List<Dice>();
        if (isAll)
        {
            result.AddRange(GetDiceLine(startPos, EDirection.LeftUp, true));
            result.AddRange(GetDiceLine(startPos, EDirection.RightUp, true));
        }
        else
        {
            result.AddRange(GetDiceLine(startPos, EDirection.LeftUp, count, true));
            result.AddRange(GetDiceLine(startPos, EDirection.RightUp, count, true));
        }
        return result.ExcludeReduplication();
    }

    public IEnumerable<Dice> GetDiceRectangle(Vector2Int centerPos, int size)
    {
        if (size % 2 == 0)
        {
            Debug.LogWarning("size가 짝수입니다. 홀수로 변환합니다.");
            size += 1;
        }
        List<Dice> result = new List<Dice>();
        Vector2Int startPos = centerPos + new Vector2Int(-(size / 2), size / 2);
        Vector2Int endPos = centerPos + new Vector2Int(size / 2, -(size / 2));
        for (int x = startPos.x; x <= endPos.x; x++)
        {
            for (int y = startPos.y; y >= endPos.y; y--)
            {
                if (TryGetDice(new Vector2Int(x, y), out Dice dice))
                {
                    result.Add(dice);
                }
            }
        }
        return result;
    }

    public IEnumerable<Dice> GetDices(Vector2 center, int[,] pattern)
    {
        return null;
    }
}
