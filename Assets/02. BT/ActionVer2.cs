using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionVer2 : Action
{
	private bool isStarted = false;

	public override TaskStatus OnUpdate()
	{
		if(!isStarted)
		{
			isStarted = true;
			OnEnter();
		}

		return base.OnUpdate();
	}

	public virtual void OnEnter()
	{

	}

	public override void OnEnd()
	{
		base.OnEnd();
		isStarted = false;
	}
}
