using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ari_RectPattern : BossPattern
{
    public override void InitPatternLinker(DicePatternLinker patternLinker)
    {
        string className = this.GetType().ToString();
        patternLinker.AddPattern(className, GetRactPattern);
    }

    private IEnumerable<Dice> GetRactPattern()
    {
        Ariadne_Unit myBoss = _bossUnit as Ariadne_Unit;
        return DiceManager.Instance.GetDiceSquare(myBoss.GetPlayerPos, 3, true);
    }

    public override void PatternStart()
    {
        _bossUnit.StartCoroutine(PatternCoroutine());
    }

    private IEnumerator PatternCoroutine()
    {
        Ariadne_Unit myBoss = _bossUnit as Ariadne_Unit;
        yield return new WaitForSeconds(myBoss._testWaitTime);
        myBoss.MoveAndAttack(myBoss.GetPlayerPos, GetRactPattern());
        yield return new WaitForSeconds(myBoss._upTime + myBoss._moveTime + myBoss._downTime + 0.1f);

        patternState = PatternState.Success;
    }

    public override BossPattern GetSubPattern()
    {
        return null;
    }
}
