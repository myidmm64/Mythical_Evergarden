using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerBase : MonoBehaviour, IDiceUnit
{
    [SerializeField] private PlayerData playerData;
    private float playerSpeed;

    [SerializeField] private Dice _dice = null;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Transform Renderer;

    private PlayerMove _playerMove;
    private PlayerInput _playerInput;
    private PlayerAttack _playerAttack; 
    private PlayerAnimation _playerAnimation;

    private PlayerState _playerState;

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
        _playerAnimation = GetComponent<PlayerAnimation>();
        _playerMove?.InitData(playerData);
        _playerInput = new PlayerInput();
        mainCamera = Camera.main;
        playerSpeed = playerData.MoveSpeed;
        _playerState = PlayerState.Idle;
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

    private void SetIdle()
    {
        _playerState = PlayerState.Idle;
        _playerAnimation.SetAnimationBoolByPlayerState(_playerState, 1);
    }

    private bool CheckIsNotIdleStateNow()
    {
        if (_playerState != PlayerState.Idle) return true;
        else return false;
    }

    void Attack()
    {
        if (CheckIsNotIdleStateNow()) return;
        _playerState = PlayerState.DefaultAttack;
        //_playerAnimation.SetAnimationBoolByPlayerState(_playerState);
        _playerAttack.StartAttack(myPos, ()=>
        {
            SetIdle();
        });
    }

    void ChangeMoveSpeed(float speed)
    {
        playerSpeed = speed;
        _playerMove.SetSpeed(playerSpeed);
    }

    void Move(Vector2Int direction)
    {
        if (CheckIsNotIdleStateNow()) return;
        _playerState = PlayerState.Move;
        SetPlayerLeftRightRotation(direction);

        float directionY = 0;
        directionY = direction.y > myPos.y? 1 : -1;
        if (direction.y == myPos.y) directionY = 0;

        _playerAnimation.PlayMove(directionY, playerSpeed);
        StartCoroutine(_playerMove.Move(direction, myPos, () => { CheckDice(direction); }, SetIdle));
    }

    private void SetPlayerLeftRightRotation(Vector2Int direction)
    {
        Vector3 vec = Vector3.one;
        if (direction.x == myPos.x) return;
        vec.x = direction.x > myPos.x ? 1 : -1;
        Renderer.localScale = vec;
    }


    private void CheckCameraRotation(Vector2Int direction)
    {
        Vector3 cameraForward = mainCamera.transform.forward;
        Vector3 cameraRight = mainCamera.transform.right;

        // 카메라의 상하 회전을 고려하지 않고 수평 방향으로 이동
        Vector2 moveDirection = new Vector2(cameraForward.x, cameraForward.z) * direction.y + new Vector2(cameraRight.x, cameraRight.z) * direction.x;
    }

    void CheckDice(Vector2Int direction)
    {
        myPos = direction;
        if (DiceManager.Instance.TryGetDice(myPos, out Dice dice))
        {
            _dice = dice;
        }
    }

}

public enum PlayerState
{
    Idle,
    Move,
    DefaultAttack,
    Skill,
    CounterAttack,
    MovingSkill,
}

