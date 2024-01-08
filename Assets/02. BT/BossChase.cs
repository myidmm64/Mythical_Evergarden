using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using BehaviorDesigner.Runtime.Tasks;

public class BossChase : DOAction
{
	public Vector3 boss_move_pos = Vector3.zero;

	private IDiceUnit player_object = null;

	private bool isCancled = false;

	public override void OnStart()
	{
		base.OnStart();
		player_object = GameObject.Find("Player").GetComponent<IDiceUnit>();
	}

	public override void OnEnter()
	{
		Dice dice;
		if (DiceManager.Instance.TryGetDice(player_object.myPos, out dice))
		{
			boss_move_pos = dice.transform.position;
			tweens = this.gameObject.transform.DOMove(boss_move_pos, time);
			base.OnEnter();
		}
		else
		{
			Debug.LogError("Don't Find Tile Object");
			isCancled = true;
		}
	}

	public override TaskStatus OnUpdate()
	{
		if(isCancled) { return TaskStatus.Failure; }

		return base.OnUpdate();
	}
}
