using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossUp : Action
{
	public SharedGameObject boss_object;

	public Dice cur_dice;

	public Vector3 yPos = Vector3.up;
	public float time;

	public bool isReturn = false;

	public override void OnStart()
	{
		if(DiceManager.Instance.TryGetDice(boss_object.Value.GetComponent<IDiceUnit>().myPos, out Dice dice))
			cur_dice = dice;
	}
	
	public override TaskStatus OnUpdate()
	{
        DG.Tweening.Sequence seq = DOTween.Sequence();
		seq.Append(this.gameObject.transform.DOMove(yPos, time));
		seq.AppendCallback(() => isReturn = true);
		seq.Play();

		if(isReturn)
			return TaskStatus.Success;
		else
			return TaskStatus.Running;
	}
}
