using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class CallEnemyRange : FsmCondition
{
	public float range;

	public LayerMask layerMask;
	public override bool IsSatisfied(FsmState curr, FsmState next)
	{
		Collider[] colliders = Physics.OverlapSphere(this.transform.position, range, layerMask);

		if (colliders.Length > 0)
		{
			Debug.Log("?????");
			return true;
		}
		else
			return false;
	}
}
