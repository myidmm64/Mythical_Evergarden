using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using UnityEngine;

public class TestBoss : MonoBehaviour, IDiceUnit
{
    public int hp = 100;
    public Dice myDice { get; set; }
    public Vector2Int myPos { get; set; }

    void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        if (this.ChangeMyDice(DiceManager.Instance.mapCenter))
        {
            transform.position = myDice.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnterDice(Dice enterdDice)
    {
        
    }

    public void ExitDice(Dice exitedDice)
    {
        
    }

}
