using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DiceManager : MonoSingleTon<DiceManager>
{
    [SerializeField]
    private Dictionary<Vector2, Dice> _dices = new Dictionary<Vector2, Dice>();
    [SerializeField]
    private TestPlayer _player = null;


    [SerializeField]
    private Vector2 _diceCenterPosition = Vector2.zero;
    [SerializeField]
    private Vector2Int _diceCounts = Vector2Int.zero;

    private void Start()
    {
        SpawnDices();
    }

    private void SpawnDices()
    {
        int width = _diceCounts.x;
        int height = _diceCounts.y;
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Dice dice = PoolManager.Instance.Pop(EPoolType.NormalDice) as Dice;
                dice.transform.position = (Vector3)_diceCenterPosition + new Vector3(i, 0, j);
                _dices.Add(new Vector2(i, j), dice);
                dice.transform.SetParent(transform, false);
            }
        }
    }

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
