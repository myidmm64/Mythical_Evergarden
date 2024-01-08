using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossYPos : DOAction
{
	public float yPos = 0;

	public override void OnStart()
	{
		base.OnStart();
	}

	public override void OnEnter()
	{
		tweens = this.gameObject.transform.DOMoveY(yPos, time);
		base.OnEnter();
	}
}
