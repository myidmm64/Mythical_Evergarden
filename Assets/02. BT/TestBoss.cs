using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using UnityEngine;

public class TestBoss : MonoBehaviour, IDiceUnit
{
    [SerializeField]
    private BehaviorTree _bt = null;

    private SharedDice _dice;

    public Dice myDice { get; set; }
    public Vector2Int myPos { get; set; }

    void Awake()
    {
        _bt = GetComponent<BehaviorTree>();
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
        if (DiceManager.Instance.TryGetDice(myPos, out Dice dice))
        {
            _dice = dice;
            _bt.SetVariable("_dice", _dice);
        }
    }

    public void EnterDice(Dice enterdDice)
    {
        
    }

    public void ExitDice(Dice exitedDice)
    {
        
    }

}
