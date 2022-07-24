using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class PlayerAttackRange : FsmCondition
{
	public float Range;

    public override bool IsSatisfied(FsmState curr, FsmState next)
	{
        return (Vector3.Distance(Player.transform.position, transform.position) < Range);
	}
}
