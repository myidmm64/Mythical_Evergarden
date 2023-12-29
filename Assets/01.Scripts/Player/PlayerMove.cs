using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private float float_moveSpeed;
    public void SetSpeed(float _speed) => float_moveSpeed = _speed;

    private bool IsCanMove = true;
    private bool IsStuckByUnit = false;
    private bool IsKnockBack;
    private bool IsCatching;

    public void InitData(PlayerData _playerData)
    {
        SetSpeed(_playerData.MoveSpeed);
    }

    public IEnumerator Move(Vector2Int direction, Vector2Int beforePos, Action CheckDice, Action SetIdle)
    {
        if(IsCanMove == false) 
        { 
            yield break; 
        }

        IsCanMove = false;
        Vector3 myPos = this.transform.position;
        Dice targetDice = null;
        if(!DiceManager.Instance.TryGetDice(direction, out targetDice))
        {
            Debug.LogWarning("No Dice Here");
            IsCanMove = true;
            SetIdle.Invoke();
            yield break;
        }
    
        float time = 0;
        while (true)
        {
            if(time == 1) 
            {
                CheckDice.Invoke();
                SetIdle.Invoke();
                IsCanMove = true;
                yield break; 
            }

            if(BattleManager.Instance.GetBossUnitOnDice(direction, out BossUnit boss) && time > 0.3)
            {
                StartCoroutine(BackToBeforeDiceMove(beforePos, SetIdle));
                yield break;
            }

            time = Mathf.Clamp(time + Time.deltaTime * float_moveSpeed, 0, 1);
            this.transform.position = Vector3.Lerp(myPos, targetDice.transform.position, EasingGraphs.EaseInOutQuint(time));
            yield return null;
        }
    }

    private IEnumerator BackToBeforeDiceMove(Vector2Int beforeDice , Action SetIdle)
    {
        Debug.Log("Blocked");
        IsCanMove = false;
        float time = 0;
        Vector3 myPos = this.transform.position;
        DiceManager.Instance.TryGetDice(beforeDice, out Dice befoceDiceVec);
        while (true)
        {
            if (time == 1)
            {
                IsCanMove = true;
                SetIdle.Invoke();
                yield break;
            }

            time = Mathf.Clamp(time + Time.deltaTime * 20, 0, 1);
            this.transform.position = Vector3.Lerp(myPos, befoceDiceVec.transform.position, EasingGraphs.EaseInOutQuint(time));
            yield return null;
        }
    }
}
