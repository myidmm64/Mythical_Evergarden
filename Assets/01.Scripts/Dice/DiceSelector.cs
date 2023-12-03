using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DiceSelector
{
    private Dictionary<Vector2, Dice> _dices = null;

    public DiceSelector(Dictionary<Vector2, Dice> dices)
    {
        if(dices == null)
        {
            Debug.LogError("dices가 null입니다.");
        }
        _dices = dices;
    }

    public Dice GetDice(int row, int column)
    {
        _dices.TryGetValue(new Vector2(row, column), out var dice);
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
