using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CallChaseCondition : FsmState, ChaseInterface
{
    private FsmLegacyAni anim;
    private GameObject targetObject;
    private NavMeshAgent agent;
    public void Chase()
	{
        agent.destination = targetObject.transform.position;
    }

	void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<FsmLegacyAni>();
        targetObject = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        Debug.Log("부르는 중");
        Chase();
    }

    public override void OnStateEnter()
    {
        anim.ChangeAnimation(FsmLegacyAni.ClipState.Move, 0.25f);
    }

    public override void OnStateLeave()
    {

    }
}
