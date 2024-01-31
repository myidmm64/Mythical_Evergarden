using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossUnit : MonoBehaviour, IDiceUnit, IBossState
{
    [SerializeField]
    private PopupDataSO _testHitPopupData = null;
    [SerializeField]
    private int _mapHp = 4000;
    [SerializeField]
    private HPBar _hpBar = null;

    private int _curHp = 0;

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
        _curHp -= damage;
        if (PopupManager.Instance != null)
        {
            PopupManager.Instance.Popup(_testHitPopupData, damage.ToString(), transform.position + Vector3.up * 0.5f, null);
        }
        if (CameraManager.Instance != null)
        {
            CameraManager.Instance.CameraShake(1f, 2f, 0.1f);
        }
        _hpBar?.HpUpdate(_curHp);
    }

    protected virtual void Start()
    {
        _curHp = _mapHp;
        _hpBar?.HpInit(_mapHp);
        Vector2Int newPos = DiceManager.Instance.mapCenter;
        if (this.ChangeMyDice(newPos, out Dice dice))
        {
            transform.position = dice.transform.position;
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
