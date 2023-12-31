using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDiceUnit
{
    public Dice myDice { get; set; }
    public Vector2Int myPos { get; set; }

    public void EnterDice(Dice enterdDice);
    public void ExitDice(Dice exitedDice);
}
