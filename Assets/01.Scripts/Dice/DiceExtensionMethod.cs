using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class DiceExtensionMethod
{
    /// <summary>
    /// IEnumerable 내의 IDiceUnit들을 반환합니다.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="dices"></param>
    /// <returns></returns>
    public static IEnumerable<IDiceUnit> GetIDiceUnits(this IEnumerable<Dice> dices)
    {
        List<IDiceUnit> result = new List<IDiceUnit>();
        foreach (var dice in dices)
        {
            if(dice.diceUnit != null)
            {
                result.Add(dice.diceUnit);
            }    
        }
        return result;
    }

    /// <summary>
    /// IEnumerable 내의 IDiceUnit들을 반환합니다.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="dices"></param>
    /// <returns></returns>
    public static IEnumerable<T> GetIDiceUnits<T>(this IEnumerable<Dice> dices) where T : IDiceUnit
    {
        List<T> result = new List<T>();
        foreach (var dice in dices)
        {
            if (dice.diceUnit != null)
            {
                if(dice.diceUnit is T)
                {
                    result.Add((T)dice.diceUnit);
                }
            }
        }
        return result;
    }

    /// <summary>
    /// IEnumerable 내의 중복되는 Dice들을 하나만 남기고 없애줍니다.
    /// </summary>
    /// <param name="dices"></param>
    /// <returns></returns>
    public static IEnumerable<Dice> ExcludeReduplication(this IEnumerable<Dice> dices)
    {
        return dices.Distinct();
    }

    /// <summary>
    /// 두 Ienumerable의 차집합을 반환합니다.
    /// </summary>
    /// <param name="dices"></param>
    /// <param name="exceptDices"></param>
    /// <returns></returns>
    public static IEnumerable<Dice> ExceptDices(this IEnumerable<Dice> dices, IEnumerable<Dice> exceptDices)
    {
        return dices.Except(exceptDices);
    }

    public static IEnumerable<Dice> AddDices(this IEnumerable<Dice> dices, params Vector2Int[] addPositions)
    {
        List<Dice> result = new List<Dice>();
        result.AddRange(dices);
        foreach (var addPosition in addPositions)
        {
            if(DiceManager.Instance.TryGetDice(addPosition, out Dice dice))
            {
                result.Add(dice);
            }
        }
        return result;
    }

    public static IEnumerable<Dice> SubDices(this IEnumerable<Dice> dices, params Vector2Int[] subPositions)
    {
        List<Dice> result = new List<Dice>();
        foreach (var subPosition in subPositions)
        {
            if (DiceManager.Instance.TryGetDice(subPosition, out Dice dice))
            {
                result.Add(dice);
            }
        }
        return (dices.ExceptDices(result)).ExcludeReduplication();
    }
}
