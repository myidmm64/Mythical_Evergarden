using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTimeIdle : Idle
{
	public int max_timer = 0;

	[SerializeField]
	private float timer = 0;

	public override TaskStatus OnUpdate()
	{
		if (max_timer > timer)
		{
			timer = 0;
			return TaskStatus.Success;
		}	
		else
		{
			timer += Time.deltaTime;
			return TaskStatus.Running;
		}
	}
}
