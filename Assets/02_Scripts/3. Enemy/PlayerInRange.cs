using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInRange : FsmCondition
{
	public float Range;

	public Transform Player;

	public override bool IsSatisfied(FsmState curr, FsmState next)
	{
		//bool isCheck = (transform.position - Player.position).sqrMagnitude <= Range * Range;
		//Debug.Log(Vector3.Distance(transform.position, Player.position));
		//float ischeck = Vector3.Distance(transform.position, Player.position);
		//Debug.Log((transform.position - Player.position).sqrMagnitude);
		//return (transform.position - Player.position).sqrMagnitude >= Range;
		return (transform.position - Player.position).sqrMagnitude >= Range;
	}
}
