using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossUnit : MonoBehaviour, IDiceUnit, IBossState
{
    public BossStat _bossStat = new BossStat();

	public Dice myDice { get; set; }
    public Vector2Int myPos { get; set; }

	public UnitState STATE { get => state; set => AddState(value); }
    public UnitState state = UnitState.None;

	public void EnterDice(Dice enterdDice)
    {

    }

    public void ExitDice(Dice exitedDice)
    {

    }

    public virtual void GetDamage(int damage)
    {
        
    }

    private void Start()
    {
        myPos = new Vector2Int(4, 4);
        if (DiceManager.Instance.TryGetDice(myPos, out Dice dice))
        {
            transform.position = dice.transform.position;
			myDice = dice;
		}

	}

	public bool HaveState(UnitState state)
	{
        if ((state & STATE) != 0)
            return true;
        else
            return false;
	}

	public void DeleteState(UnitState state) => this.state |= state;

	public void AddState(UnitState state) => this.state &= ~state;

	public void DelAllState()
	{
        this.state = UnitState.None;
	}

    /// <summary>
    /// state�� ���� ������ �ڱ� �ڽ��� �����ָ� ���� ����
    /// </summary>
    /// <param name="state"></param>
    public void NegaState(UnitState state)
	{
		this.state ^= state;
	}
}

public interface IBossState
{
    public UnitState STATE { get; set; }

    public bool HaveState(UnitState state);
    public void DeleteState(UnitState state);
    public void AddState(UnitState state);
    public void DelAllState();
    public void NegaState(UnitState state);
}

[Serializable]
public struct BossStat
{
    public int hp;
    public int atk;
}

[Flags]
public enum UnitState
{
    Empty = 0,
    None = 1 << 0,
    GodTime = 1 << 1,

}
