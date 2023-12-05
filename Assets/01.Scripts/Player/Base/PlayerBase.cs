using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour, IDiceUnit
{
    [SerializeField] PlayerData playerData;

    private PlayerMove _playerMove;

    public Dice myDice { get; set; }
    public Vector2Int myPos { get; set; }

    public void EnterDice()
    {

    }

    public void ExitDice()
    {
        _playerMove = new PlayerMove(playerData);
    }

    void Awake()
    {

    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
