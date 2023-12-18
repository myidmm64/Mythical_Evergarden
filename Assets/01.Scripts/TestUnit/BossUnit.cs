using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossUnit : MonoBehaviour, IDiceUnit
{
    public int Hp = 100;
    public int Atk = 10;

    public Dice myDice { get; set; }
    public Vector2Int myPos { get; set; }

    public void EnterDice(Dice enterdDice)
    {

    }

    public void ExitDice(Dice exitedDice)
    {

    }

    public virtual void GetDamage(int damage)
    {

    }

    private void Start()
    {
        myPos = new Vector2Int(4, 4);
        if (DiceManager.Instance.TryGetDice(myPos, out Dice dice))
        {
            transform.position = dice.transform.position;
        }

    }
}
