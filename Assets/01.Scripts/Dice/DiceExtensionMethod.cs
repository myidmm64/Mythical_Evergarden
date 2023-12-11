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
}
