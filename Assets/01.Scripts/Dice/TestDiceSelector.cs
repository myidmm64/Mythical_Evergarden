using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class TestDiceSelector : MonoBehaviour
{
    int i = 0;
    DicePatternBuilder _dpb = new DicePatternBuilder();
    private Vector2Int myPos = new Vector2Int(4, 4);

    private void Start()
    {
        _dpb.AddPattern("Test1", MyPattern0);
        _dpb.AddPattern("Test1", MyPattern1);
        _dpb.AddPattern("Test1", MyPattern2);

        _dpb.ChangePattern("Test1");
    }

    public IEnumerable<Dice> MyPattern0()
    {
        return DiceManager.Instance.GetDiceRotatedSquare(myPos, 1);
    }

    public IEnumerable<Dice> MyPattern1()
    {
        return DiceManager.Instance.GetDiceRotatedSquare(myPos, 2);
    }

    public IEnumerable<Dice> MyPattern2()
    {
        return DiceManager.Instance.GetDiceRotatedSquare(myPos, 3);
    }

    private void Update()
    {
        Vector2Int centerPos = DiceManager.Instance.mapCenter;
        if (Input.GetKeyDown(KeyCode.E))
        {
            DD(_dpb.Next());
            //DD(DiceManager.Instance.GetDiceSquare(centerPos, 1));
            //DD(DiceManager.Instance.GetDicesWithPattern(centerPos, "111\n110\n010", EDirection.Left)) ;
        }
    }

    public void DD(IEnumerable<Dice> dices)
    {
        if (dices == null) return;

        foreach (var dice in dices)
        {
            dice.RollDiceWithRandom(1, 7);
            dice.ColorAnimation();
        }
    }
}
