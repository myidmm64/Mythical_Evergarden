using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDiceSelector : MonoBehaviour
{
    private void Update()
    {
        Vector2Int centerPos = DiceManager.Instance.mapCenter;
        if (Input.GetKeyDown(KeyCode.Q))
        {
            DD(DiceManager.Instance.GetSamePipDices(3));
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            //DD(DiceManager.Instance.GetDiceSquare(centerPos, 1));
            DD(DiceManager.Instance.GetDiceRotatedSquare(centerPos, Random.Range(1,6)));
        }
    }

    public void DD(IEnumerable<Dice> dices)
    {
        foreach (var dice in dices)
        {
            dice.RollDiceWithRandom(1, 7);
            dice.ColorAnimation();
        }
    }
}
