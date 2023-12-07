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

    public IEnumerator Move(Vector3 myPos, Vector3 direction , Action callBack)
    {
        float time = 0;
        while (true)
        {
            if(time == 1) 
            {
                callBack.Invoke();
                yield break; 
            }


            time = Mathf.Clamp(time + Time.deltaTime * float_moveSpeed, 0, 1);
            this.transform.position = Vector3.Lerp(myPos, direction, EasingGraphs.EaseInOutCirc(time));
            yield return null;
        }

    }
}
