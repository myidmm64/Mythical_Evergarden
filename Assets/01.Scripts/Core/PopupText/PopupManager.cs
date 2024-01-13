using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class PopupManager : MonoSingleTon<PopupManager>
{
    [SerializeField]
    private PopupDataSO _defaultData = null;

    public PopupPoolObject Popup(PopupDataSO data, string text, Vector2 pos, Action Callback = null)
    {
        if (data == null)
            data = _defaultData;
        PopupPoolObject popupPoolObj = PoolManager.Instance.Pop<PopupPoolObject>(EPoolType.PopupText);
        switch (data.popupType)
        {
            case PopupType.None:
                break;
            case PopupType.Up:
                popupPoolObj.PopupText(data, text, pos, pos + Vector2.up * 0.5f, Callback);
                break;
            case PopupType.Punch:
                popupPoolObj.PunchPopup(data, text, pos, Callback);
                break;
            case PopupType.Drop:
                break;
            default:
                break;
        }
        return popupPoolObj;
    }
}

[System.Serializable]
public enum PopupType
{
    None,
    Up,
    Punch,
    Drop
}