using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BombChase : FsmState,ChaseInterface
{
    [SerializeField, Header("�Ѿư��� Ÿ��(�÷��̾�)")]
    Transform target;

    private Vector3 lastKnownLoc;
    private NavMeshAgent agent;
    FsmLegacyAni anim;

    bool isChase = false;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<FsmLegacyAni>();
    }

    void Update()
    {
        if(isChase)
        {
            Chase();
        }
        else
            ChangeStop(true);

    }

    public void Chase()
	{
        ChangeStop(false);
        agent.destination = lastKnownLoc = target.position;
    }

    public void ChangeStop(bool value)
    {
        agent.isStopped = value;
    }

    public override void OnStateEnter()
    {
        isChase = true;
        anim.ChangeAnimation(FsmLegacyAni.ClipState.Move, 0.25f);
        agent.destination = lastKnownLoc = target.position;
    }

    public override void OnStateLeave()
    {
        isChase = false;
        agent.ResetPath();
        this.GetComponent<BombChase>().enabled = false;
    }

    public Vector3 GetLastKnownPlayerLocation()
    {
        return lastKnownLoc;
    }

}
