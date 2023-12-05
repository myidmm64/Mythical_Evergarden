using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove
{
    public PlayerMove(PlayerData _playerData)
    {
        InitData(_playerData);
    }

    private float float_moveSpeed;
    public void SetSpeed(float _speed) => float_moveSpeed = _speed;

    public void InitData(PlayerData _playerData)
    {
        SetSpeed(_playerData.MoveSpeed);
    }

    public IEnumerator Move()
    {
        float moveTime = 1 / float_moveSpeed;
        while(moveTime < 1)
        {
            
        }
        yield return null;
    }
}
