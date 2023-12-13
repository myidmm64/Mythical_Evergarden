using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField]
    private Animator animator_Player;

    private bool isMoving;
    private bool isAttacking;
    private bool isDoSkill;
    private bool isCounterAttacking;

    private void Awake()
    {
        SetAnimatorBool();
    }

    private void SetAnimatorBool()
    {
        animator_Player.SetBool(PlayerState.Move.ToString(), isMoving);
        animator_Player.SetBool(PlayerState.DefaultAttack.ToString(), isAttacking);
        animator_Player.SetBool(PlayerState.Skill.ToString(), isDoSkill);
        animator_Player.SetBool(PlayerState.CounterAttack.ToString(), isCounterAttacking);
    }

    public void PlayMove(float directionY, float speed)
    {
        animator_Player.SetFloat("PlayerDirection", directionY);
        SetAnimationBoolByPlayerState(PlayerState.Move, speed);
    }

    public void SetAnimationBoolByPlayerState(PlayerState playerState, float speed)
    {
        SetBoolReset();
        SetAnimatorBool();
        animator_Player.speed = 1 * speed;

        switch (playerState)
        {
            case PlayerState.Idle:
                SetBoolReset();
                SetAnimatorBool();
                break;
            case PlayerState.Move:
                isMoving = true;
                animator_Player.SetBool(playerState.ToString(), isMoving);
                break;
            case PlayerState.DefaultAttack:
                isAttacking = true;
                animator_Player.SetBool(playerState.ToString(), isAttacking);
                break;
            case PlayerState.Skill:
                isDoSkill = true;
                animator_Player.SetBool(playerState.ToString(), isDoSkill);
                break;
            case PlayerState.CounterAttack:
                isCounterAttacking = true;
                animator_Player.SetBool(playerState.ToString(), isCounterAttacking);
                break;
        }

    }

    private void SetBoolReset()
    {
        isMoving = false;
        isAttacking = false;
        isDoSkill = false;
        isCounterAttacking = false;
    }
}
