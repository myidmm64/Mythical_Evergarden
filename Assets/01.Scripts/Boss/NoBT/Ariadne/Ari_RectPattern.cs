using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ari_RectPattern : BossPattern
{
    public override void InitPatternLinker(DicePatternLinker patternLinker)
    {
        patternLinker.AddPattern(_className, GetRactPattern);
    }

    private IEnumerable<Dice> GetRactPattern()
    {
        Ariadne_Unit myBoss = _bossUnit as Ariadne_Unit;
        return DiceManager.Instance.GetDiceSquare(myBoss.GetPlayerPos, 3, true);
    }

    public override void PatternStart()
    {
        _patternLinker.ChangePattern(_className);
        _bossUnit.StartCoroutine(PatternCoroutine());
    }

    protected override IEnumerator PatternCoroutine()
    {
        Ariadne_Unit myBoss = _bossUnit as Ariadne_Unit;
        yield return new WaitForSeconds(myBoss._testWaitTime);
        yield return myBoss.MoveAndAttack(myBoss.GetPlayerPos, _patternLinker.Next()).WaitForCompletion();

        patternState = PatternState.Success;
    }

    public override BossPattern GetSubPattern()
    {
        return null;
    }
}
