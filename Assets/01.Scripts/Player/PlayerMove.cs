using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private float float_moveSpeed;
    public void SetSpeed(float _speed) => float_moveSpeed = _speed;

    public void InitData(PlayerData _playerData)
    {
        SetSpeed(_playerData.MoveSpeed);
    }

    public IEnumerator Move(Vector2Int myPos, Vector2Int direction , Action callBack)
    {
        float time = 0;
        Vector2 moveVec = new Vector2();
        Vector2 targetVec = new Vector2();

        while (time != 1)
        {
            time += Mathf.Clamp(Time.deltaTime * float_moveSpeed,0,1);
            moveVec.x = myPos.x;
            moveVec.y = myPos.y;

            targetVec.x = direction.x;
            targetVec.y = direction.y;

            transform.position = Vector2.Lerp(moveVec, targetVec, EasingGraphs.EaseOutCirc(time));
            yield return null;
        }

        callBack.Invoke();
    }
}
