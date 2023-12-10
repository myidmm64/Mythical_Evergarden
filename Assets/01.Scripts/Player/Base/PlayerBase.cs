using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerBase : MonoBehaviour, IDiceUnit
{
    [SerializeField] private PlayerData playerData;

    [SerializeField] private Dice _dice = null;


    private PlayerMove _playerMove;
    private PlayerInput _playerInput;
    private PlayerAttack _playerAttack;

    public Dice myDice { get; set; }
    public Vector2Int myPos { get; set; }

    public void EnterDice(Dice EnterDice)
    {

    }

    public void ExitDice(Dice ExitDice)
    {
    }

    void Awake()
    {
        _playerMove = GetComponent<PlayerMove>();
        _playerAttack = GetComponent<PlayerAttack>();
        _playerMove?.InitData(playerData);
        _playerInput = new PlayerInput();
    }

    void Start()
    {
        if(InputManager.Instance != null)
        {
            KeySetting();
        }

        BattleManager.Instance.AddPlayerUnit(this);

        myPos = new Vector2Int(1, 1);
        if (DiceManager.Instance.TryGetDice(myPos, out Dice dice))
        {
            transform.position = dice.transform.position;
        }
    }

    private async void KeySetting()
    {
        var task = Task.Run(()=>_playerInput.SetKey());
        await task;
        SetActions();
    }

    private void SetActions()
    {
        _playerInput.SetMoveActions(InputKeyTypes.MoveUp, () => Move(myPos + Utility.GetDirection(EDirection.Up)));
        _playerInput.SetMoveActions(InputKeyTypes.MoveDown, () => Move(myPos + Utility.GetDirection(EDirection.Down)));
        _playerInput.SetMoveActions(InputKeyTypes.MoveLeft, () => Move(myPos + Utility.GetDirection(EDirection.Left)));
        _playerInput.SetMoveActions(InputKeyTypes.MoveRight, () => Move(myPos + Utility.GetDirection(EDirection.Right)));

        _playerInput.SetAttackAction(InputKeyTypes.Attack_Default, Attack);
        _playerInput.SetAttackAction(InputKeyTypes.Skill, () => { });
        _playerInput.SetAttackAction(InputKeyTypes.Attack_Counter, () => { });
    }


    void Move(Vector2Int direction)
    {
        StartCoroutine(_playerMove.Move(direction, myPos, ()=> CheckDice(direction)));
    }

    void Attack()
    {
        _playerAttack.StartAttack(myPos);
    }

    void CheckDice(Vector2Int direction)
    {
        myPos = direction;
        Debug.Log(myPos);
        if (DiceManager.Instance.TryGetDice(myPos, out Dice dice))
        {
            _dice = dice;
        }
    }

}
