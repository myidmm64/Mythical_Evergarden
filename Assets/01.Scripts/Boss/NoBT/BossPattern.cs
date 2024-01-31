using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossPattern : MonoBehaviour
{
    [SerializeField]
    protected List<BossPattern> _subPatterns = new List<BossPattern>();

    protected string _className = "";
    protected DicePatternLinker _patternLinker = null;
    protected BossUnit _bossUnit = null;

    public PatternState patternState = PatternState.None;

    public void InitPattern(BossUnit bossUnit, DicePatternLinker patternLinker)
    {
        _className = this.GetType().ToString();
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
    protected virtual IEnumerator PatternCoroutine()
    {
        yield break;
    }
}

public enum PatternState
{
    None,
    Running,
    Success
}