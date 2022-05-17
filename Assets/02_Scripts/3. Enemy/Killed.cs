using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killed : FsmCondition
{
	private bool dead;

	public bool Dead
	{
		get { return dead; }
		set { dead = value; }
	}

	public override bool IsSatisfied(FsmState curr, FsmState next)
	{
		return false && !(curr is EnemyDying);
	}
}
