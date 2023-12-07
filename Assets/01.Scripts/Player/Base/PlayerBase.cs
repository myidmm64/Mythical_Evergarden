using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour, IDiceUnit
{
    [SerializeField] private PlayerData playerData;

    [SerializeField] private Dice _dice = null;


    private PlayerMove _playerMove;
    private PlayerInput _playerInput;

    public Dice myDice { get; set; }
    public Vector2Int myPos { get; set; }

    public void EnterDice()
    {

    }

    public void ExitDice()
    {
    }

    void Awake()
    {
        _playerMove = GetComponent<PlayerMove>();
        _playerMove?.InitData(playerData);
        _playerInput = new PlayerInput();
    }

    void Start()
    {
        SetKeys();

        InputManager.Instance.SetAction(InputKeyTypes.MoveUp, () => Move(myPos + Utility.GetDirection(EDirection.Up)));
        InputManager.Instance.SetAction(InputKeyTypes.MoveDown, () => Move(myPos + Utility.GetDirection(EDirection.Down)));
        InputManager.Instance.SetAction(InputKeyTypes.MoveLeft, () => Move(myPos + Utility.GetDirection(EDirection.Left)));
        InputManager.Instance.SetAction(InputKeyTypes.MoveRight, () => Move(myPos + Utility.GetDirection(EDirection.Right)));

        myPos = new Vector2Int(1, 1);
        if (DiceManager.Instance.TryGetDice(myPos, out Dice dice))
        {
            transform.position = dice.transform.position;
        }
    }

    void Move(Vector2Int direction)
    {
        if (DiceManager.Instance.TryGetDice(direction, out Dice dice))
        {
            StartCoroutine(_playerMove.Move(_playerMove.transform.position, dice.transform.position, () => CheckDice(direction)));
        }
    }

    void CheckDice(Vector2Int direction)
    {
        myPos = direction;
        if (DiceManager.Instance.TryGetDice(myPos, out Dice dice))
        {
            _dice = dice;
        }
    }

    void SetKeys()
    {
        InputManager.Instance.SetKeyCode(InputKeyTypes.MoveUp, KeyCode.UpArrow);
        InputManager.Instance.SetKeyCode(InputKeyTypes.MoveDown, KeyCode.DownArrow);
        InputManager.Instance.SetKeyCode(InputKeyTypes.MoveLeft, KeyCode.LeftArrow);
        InputManager.Instance.SetKeyCode(InputKeyTypes.MoveRight, KeyCode.RightArrow);

        InputManager.Instance.SetKeyCode(InputKeyTypes.MoveUp, KeyCode.W);
        InputManager.Instance.SetKeyCode(InputKeyTypes.MoveDown, KeyCode.S);
        InputManager.Instance.SetKeyCode(InputKeyTypes.MoveLeft, KeyCode.A);
        InputManager.Instance.SetKeyCode(InputKeyTypes.MoveRight, KeyCode.D);

        _playerInput.InitAction();
    }
}
