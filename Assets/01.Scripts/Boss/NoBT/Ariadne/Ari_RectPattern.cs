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
        return DiceManager.Instance.GetDiceSquare(_bossUnit.myPos, 3, true);
    }

    public override void PatternStart()
    {
        _bossUnit.StartCoroutine(PatternCoroutine());
    }

    private IEnumerator PatternCoroutine()
    {
        yield return new WaitForSeconds(1f);

        Ariadne_Unit myBoss = _bossUnit as Ariadne_Unit;
        yield return new WaitUntil(() => !myBoss.MoveAndAttack(myBoss.GetPlayerPos, GetRactPattern()).IsPlaying());

        patternState = PatternState.Success;
    }

    public override BossPattern GetSubPattern()
    {
        return null;
    }
}
