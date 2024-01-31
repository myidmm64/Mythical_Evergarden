using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class BossPattern : MonoBehaviour
{
    protected DicePatternLinker _patternLinker = null;
    protected BossUnit _bossUnit = null;

    public PatternState patternState = PatternState.None;

    public virtual void InitPattern(BossUnit bossUnit, DicePatternLinker patternLinker)
    {
        _bossUnit = bossUnit;
        _patternLinker = patternLinker;
        InitPatternLinker(patternLinker);
    }

    public abstract void InitPatternLinker(DicePatternLinker patternLinker);
    public abstract void PatternStart();
    public virtual BossPattern GetSubPattern()
    {
        return null;
    }
}

public enum PatternState
{
    None,
    Running,
    Success
}