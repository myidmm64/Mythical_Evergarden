using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInput
{
    public void SetKey()
    {
        InputManager.Instance.SetKeyCode(InputKeyTypes.MoveUp, KeyCode.UpArrow);
        InputManager.Instance.SetKeyCode(InputKeyTypes.MoveDown, KeyCode.DownArrow);
        InputManager.Instance.SetKeyCode(InputKeyTypes.MoveLeft, KeyCode.LeftArrow);
        InputManager.Instance.SetKeyCode(InputKeyTypes.MoveRight, KeyCode.RightArrow);

        InputManager.Instance.SetKeyCode(InputKeyTypes.Attack_Default, KeyCode.Z);
        InputManager.Instance.SetKeyCode(InputKeyTypes.Skill, KeyCode.Space);
        InputManager.Instance.SetKeyCode(InputKeyTypes.Attack_Counter, KeyCode.C);

        InitAction();
    }
    private void InitAction()
    {
        InputManager.Instance.InputActionDictionary.Add(InputKeyTypes.MoveUp, InputAction_UpperMove);
        InputManager.Instance.InputActionDictionary.Add(InputKeyTypes.MoveDown, InputAction_LowerMove);
        InputManager.Instance.InputActionDictionary.Add(InputKeyTypes.MoveLeft, InputAction_LeftMove);
        InputManager.Instance.InputActionDictionary.Add(InputKeyTypes.MoveRight, InputAction_RightMove);

        InputManager.Instance.InputActionDictionary.Add(InputKeyTypes.Attack_Default, InputAction_Attack);
        InputManager.Instance.InputActionDictionary.Add(InputKeyTypes.Skill, InputAction_WeaponSkill);
        InputManager.Instance.InputActionDictionary.Add(InputKeyTypes.Attack_Counter, InputAction_CounterAttack);


    }

    public void SetMoveActions(InputKeyTypes type, Action action)
    {
        InputManager.Instance.SetAction(type, action);
    }

    public void SetAttackAction(InputKeyTypes type, Action action)
    {
        InputManager.Instance.SetAction(type, action);
    }

    public Action InputAction_UpperMove;
    public Action InputAction_LowerMove;
    public Action InputAction_LeftMove;
    public Action InputAction_RightMove;
    public Action InputAction_Attack;
    public Action InputAction_WeaponSkill;
    public Action InputAction_CounterAttack;

}
