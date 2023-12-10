using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoSingleTon<BattleManager>
{
    [SerializeField]
    private PlayerBase playerUnit;
    public IDiceUnit PlayerUnit => playerUnit;

    [SerializeField]
    private Dictionary<int, BossUnit> BossUnits = new Dictionary<int,BossUnit>();

    public void Awake()
    {
        Init();
    }

    private void Init()
    {
        BossUnits.Clear();
    }

    public void AddPlayerUnit(IDiceUnit diceUnit)
    {
        if(diceUnit is PlayerBase)
        {
            playerUnit = diceUnit as PlayerBase;
        }
        else
        {
            Debug.LogError("Not a PlayerUnit");
        }
    }

    public void AddBossUnit(IDiceUnit diceUnit)
    {
        if (diceUnit is BossUnit)
        {
            BossUnits.Add(diceUnit.GetHashCode(), diceUnit as BossUnit);
        }
        else
        {
            Debug.LogError("Not a PlayerUnit");
        }
    }

    public void RemoveBossUnit(IDiceUnit diceUnit)
    {
        if(BossUnits.ContainsKey(diceUnit.GetHashCode()))
        {
            BossUnits.Remove(diceUnit.GetHashCode());
        }
    }

    public bool GetBossUnitOnDice(Vector2Int pos, out BossUnit _Boss)
    {
        foreach(var boss in BossUnits.Values)
        {
            if (boss.myPos == pos)
            {
                _Boss = boss;
                return _Boss;
            }
        }

        _Boss = null;
        return _Boss;
    }
}
