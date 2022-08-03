using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallEnemy : FsmState
{
	public float range;

	public LayerMask layerMask;
	private FsmLegacyAni fsmLegacyAni;
	private void Awake()
	{
		fsmLegacyAni = GetComponent<FsmLegacyAni>();
	}
	public override void OnStateEnter()
	{
		fsmLegacyAni.ChangeAnimation(FsmLegacyAni.ClipState.Reconnaissance, 0.25f);
		Collider[] colliders = Physics.OverlapSphere(this.transform.position, range, layerMask);
		for (int i = 0; i < colliders.Length; i++)
		{
			FsmCore fsm = colliders[i].GetComponent<FsmCore>();
			for (int j = 0; j < fsm.States.Length; j++)
			{
				if (fsm.States[j].State is ChaseInterface)
				{
					fsm.ChangeState(fsm.States[j].State);
					break;
				}
			}
		}
	}
	public override void OnStateLeave()
	{

	}
}
