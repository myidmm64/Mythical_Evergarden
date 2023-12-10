using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class DiceExtensionMethod
{
    /// <summary>
    /// IEnumerable ���� �ߺ��Ǵ� Dice���� �ϳ��� ����� �����ݴϴ�.
    /// </summary>
    /// <param name="dices"></param>
    /// <returns></returns>
    public static IEnumerable<Dice> ExcludeReduplication(this IEnumerable<Dice> dices)
    {
        return dices.Distinct();
    }

    /// <summary>
    /// �� Ienumerable�� �������� ��ȯ�մϴ�.
    /// </summary>
    /// <param name="dices"></param>
    /// <param name="exceptDices"></param>
    /// <returns></returns>
    public static IEnumerable<Dice> ExceptDices(this IEnumerable<Dice> dices, IEnumerable<Dice> exceptDices)
    {
        return dices.Except(exceptDices);
    }
}
