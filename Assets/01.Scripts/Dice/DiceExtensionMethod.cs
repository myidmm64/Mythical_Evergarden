using System.Collections;
using System.Collections.Generic;
using System.Linq;

public static class DiceExtensionMethod
{
    public static IEnumerable<Dice> ExcludeReduplication(this IEnumerable<Dice> dices)
    {
        return dices.Distinct();
    }
}
