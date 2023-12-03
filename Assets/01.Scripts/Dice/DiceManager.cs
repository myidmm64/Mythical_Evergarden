using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class DiceManager : MonoSingleTon<DiceManager>
{
    [SerializeField]
    private Dictionary<Vector2, Dice> _dices = new Dictionary<Vector2, Dice>();
    [SerializeField]
    private TestPlayer _player = null;

    [TextArea]
    public string _diceMapStr = null;
    [SerializeField]
    private Vector2 _diceCenterPosition = Vector2.zero;
    [SerializeField]
    private Vector2 _dicePositionPadding = Vector2.zero;

    private void Start()
    {
        GenerateDices(_diceMapStr);
    }

    private void GenerateDices(string map)
    {
        string[] rows = map.Split('\n');
        int column = rows.Length;
        int row = rows[0].Length;
        Vector2 startPos = _diceCenterPosition + GetPaddingPos(-new Vector2(row / 2, column / 2));

        for (int i = 1; i <= column; i++)
        {
            for (int j = 1; j <= row; j++)
            {
                int number = rows[i - 1][j - 1] - '0';
                Dice dice = GetDice((EDiceType)number);
                if (dice == null) continue;

                Vector2 diceKey = new Vector2(j, column - i + 1);
                dice.diceKey = diceKey;

                Vector2 dicePosition = startPos + (new Vector2(j - 1, column - i) * _dicePositionPadding);
                dice.transform.position = dicePosition;
                dice.transform.SetParent(transform, false);

                dice.InitDice();
                _dices.Add(diceKey, dice);
            }
        }
    }

    private Vector2 GetPaddingPos(Vector2 pos)
    {
        return pos * _dicePositionPadding;
    }

    private Dice GetDice(EDiceType diceType) => diceType switch
    {
        EDiceType.None => null,
        EDiceType.NormalDice => PoolManager.Instance.Pop(EPoolType.NormalDice) as Dice,
        _ => null
    };

    public Dice GetNearDice(Vector2 position)
    {
        Vector2Int intPosition = Vector2Int.FloorToInt(position);
        _dices.TryGetValue(intPosition, out var dice);
        return dice;
    }

    public IEnumerable<Dice> GetSamePipDices(int dicePip)
    {
        var query = from dice in _dices.Values
                    where dice.dicePip == dicePip
                    select dice;
        return query;
    }

    public IEnumerable<Dice> GetDiceRow(int row)
    {
        var query = from diceKeyValue in _dices
                    where diceKeyValue.Key.y == row
                    select diceKeyValue.Value;
        return query;
    }

    public IEnumerable<Dice> GetDiceColumn(int column)
    {
        var query = from diceKeyValue in _dices
                    where diceKeyValue.Key.x == column
                    select diceKeyValue.Value;
        return query;
    }

    public IEnumerable<Dice> GetDices(Vector2 center, int[,] pattern)
    {
        return null;
    }
}
