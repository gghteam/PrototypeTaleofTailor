using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class PlayerInRange : FsmCondition
{
	public float Range;

	private Chase chaseState;

    private void Start()
    {
		chaseState = GetComponent<Chase>();
    }

    public override bool IsSatisfied(FsmState curr, FsmState next)
	{
		//bool isCheck = (transform.position - Player.position).sqrMagnitude <= Range * Range;
		//Debug.Log(Vector3.Distance(transform.position, Player.position));
		//float ischeck = Vector3.Distance(transform.position, Player.position);
		//Debug.Log((transform.position - Player.position).sqrMagnitude);
		//return (transform.position - Player.position).sqrMagnitude >= Range;
		return (Vector3.Distance(transform.position, Player.transform.position) >= Range) && (chaseState.TransitionTime <= 0);
	}

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
		Gizmos.color = Color.white;
		Gizmos.DrawWireSphere(transform.position, Range);
		Gizmos.color = Color.white;
    }
#endif
}
