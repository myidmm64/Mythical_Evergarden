using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput
{
    public void InitAction()
    {
        InputManager.Instance.InputActionDictionary.Add(InputKeyTypes.MoveUp, InputAction_UpperMove);
        InputManager.Instance.InputActionDictionary.Add(InputKeyTypes.MoveDown, InputAction_LowerMove);
        InputManager.Instance.InputActionDictionary.Add(InputKeyTypes.MoveLeft, InputAction_LeftMove);
        InputManager.Instance.InputActionDictionary.Add(InputKeyTypes.MoveRight, InputAction_RightMove);
    }


    public Action InputAction_UpperMove;
    public Action InputAction_LowerMove;
    public Action InputAction_LeftMove;
    public Action InputAction_RightMove;
    public Action InputAction_Attack;
    public Action InputAction_WeaponSkill;
    public Action InputAction_CounterSkill;

}
