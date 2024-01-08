using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;

public class BossChase : DOAction
{
	public Vector2 boss_move_pos = Vector3.zero;

	private PlayerBase player_object = null;

	public override void OnStart()
	{
		base.OnStart();
		player_object = GameObject.FindObjectOfType<PlayerBase>();
	}

	public override void OnEnter()
	{
		base.OnEnter();
		boss_move_pos = player_object.myPos;
		tweens = this.gameObject.transform.DOMove(boss_move_pos, time);
	}
}
