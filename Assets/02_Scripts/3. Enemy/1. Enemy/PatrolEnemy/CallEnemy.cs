using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallEnemy : FsmState
{
	public float range;

	public LayerMask layerMask;
	public override void OnStateEnter()
	{
		Collider[] colliders = Physics.OverlapSphere(this.transform.position, range, layerMask);
		for (int i = 0; i < colliders.Length; i++)
		{
			FsmCore fsm = colliders[i].GetComponent<FsmCore>();
			for (int j = 0; j < fsm.States.Length; j++)
			{
				fsm.ChangeState(fsm.States[j].State?.GetComponent<Chase>());
			}
		}
	}
	public override void OnStateLeave()
	{

	}
}
