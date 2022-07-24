using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallEnemy : FsmState
{

	public float range;

	public LayerMask layerMask;

	public AnimationClip CallAnimClip = null;
	private Animation skullAnimation = null;

	private void Awake()
	{
		skullAnimation = GetComponent<Animation>();

		skullAnimation[CallAnimClip.name].wrapMode = WrapMode.Loop;
	}
	public override void OnStateEnter()
	{
		skullAnimation.CrossFade(CallAnimClip.name);
		Collider[] colliders = Physics.OverlapSphere(this.transform.position, range, layerMask);
		for (int i = 0; i < colliders.Length; i++)
		{
			FsmCore fsm = colliders[i].GetComponent<FsmCore>();
			for (int j = 0; j < fsm.States.Length; j++)
			{
				if (fsm.States[j].State is Chase)
				{
					Debug.Log(fsm.States[j].State);
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
