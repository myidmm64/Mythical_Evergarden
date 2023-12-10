using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoSingleTon<InputManager>
{
    public Dictionary<InputKeyTypes, KeyCode> keyDictionary = new Dictionary<InputKeyTypes, KeyCode>();
    public Dictionary<InputKeyTypes, Action> InputActionDictionary = new Dictionary<InputKeyTypes, Action>();

    public void SetKeyCode(InputKeyTypes keyType, KeyCode keyCode)
    {
        if(!keyDictionary.TryGetValue(keyType, out KeyCode keys))
        {
            keyDictionary.Add(keyType, keyCode);
        }
    }

    public void SetAction(InputKeyTypes keyType, Action action)
    {
        if (InputActionDictionary.ContainsKey(keyType))
        {
            InputActionDictionary[keyType] -= action;
            InputActionDictionary[keyType] += action;
        }
    }

    void Update()
    {
        foreach(var key in keyDictionary.Keys)
        {
            if (Input.GetKeyDown(keyDictionary[key]))
            {
                InputActionDictionary[key].Invoke();
            }
        }
    }
}

public enum InputKeyTypes
{
    MoveUp, 
    MoveDown, 
    MoveLeft, 
    MoveRight,
    Attack_Default,
    Attack_Counter,
    Skill,
}

