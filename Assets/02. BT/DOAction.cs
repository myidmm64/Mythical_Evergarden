using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DOAction : ActionVer2
{
	protected DG.Tweening.Sequence seq = DOTween.Sequence();
	protected Tween tweens;

	public SharedGameObject boss_object;

	public Dice cur_dice;

	public float time;

	public bool isReturn = false;

	public override void OnStart()
	{
		base.OnStart();
	}

	public override void OnEnter()
	{
		boss_object.SetValue(this.gameObject);

		if (DiceManager.Instance.TryGetDice(boss_object.Value.GetComponent<IDiceUnit>().myPos, out Dice dice))
			cur_dice = dice;

		seq = DOTween.Sequence();
		seq.Append(tweens);
		seq.AppendCallback(() => isReturn = true);
		seq.Play();
	}

	public override TaskStatus OnUpdate()
	{
		base.OnUpdate();

		if (isReturn)
		{
			Debug.Log("Return DoAction Success");
			return TaskStatus.Success;
		}
		else
			return TaskStatus.Running;
	}
}
