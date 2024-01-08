using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoMove : DOAction
{
	public Vector3 boss_move_pos = Vector3.zero;

	public override void OnStart()
	{
		base.OnStart();
		tweens = this.gameObject.transform.DOMove(boss_move_pos, time);
	}

}
