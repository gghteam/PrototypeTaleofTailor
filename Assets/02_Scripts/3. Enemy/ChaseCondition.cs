using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class ChaseCondition : FsmState, ChaseInterface
{
	//public Transform Player;

	private NavMeshAgent agent;
	private Vector3 lastKnownLoc;
	private FsmLegacyAni fsmLegacyAni;

	void Awake()
	{
		agent = GetComponent<NavMeshAgent>();
		fsmLegacyAni = GetComponent<FsmLegacyAni>();
	}

	// Update is called once per frame
	void Update()
	{
		Chase();
	}

	public void Chase()
	{
		agent.destination = lastKnownLoc = Define.Player.transform.position;
	}

	public override void OnStateLeave()
	{
		agent.ResetPath();
	}

	public Vector3 GetLastKnownPlayerLocation()
	{
		return lastKnownLoc;
	}
	public override void OnStateEnter()
    {
        Debug.Log("Chase");
		fsmLegacyAni.ChangeAnimation(FsmLegacyAni.ClipState.Move, 0.3f);
	}
}
