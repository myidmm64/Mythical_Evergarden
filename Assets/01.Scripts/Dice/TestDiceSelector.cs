using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class TestDiceSelector : MonoBehaviour
{
    int i = 0;
    private Vector2Int myPos = new Vector2Int(4, 4);

    DicePatternLinker _dpl = new DicePatternLinker();
    private readonly string patternName1 = "Test1";
    private readonly string patternName2 = "Test2";

    private void Start()
    {
        _dpl.AddPattern(patternName1, MyPattern0);
        _dpl.AddPattern(patternName1, MyPattern1);
        _dpl.AddPattern(patternName2, MyPattern2);

        _dpl.ChangePattern("Test1");

        //InvokeRepeating("ChangeTestPattern", 1f, 1f);
    }

    public void PlayTestPattern()
    {
        var currentPattern = _dpl.Next();
        if (currentPattern == null)
        {
            _dpl.ChangePattern(patternName2);
        }
        foreach (var dice in currentPattern)
        {
            dice.RollDiceWithRandom(1, 7);
            dice.ColorAnimation();
        }
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
            DD(_dpl.Next());
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
