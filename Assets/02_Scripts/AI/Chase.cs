using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chase : FsmState
{
    //[SerializeField, Header("추격 속도")]
    //float chaseSpeed;

    //[SerializeField, Header("근접 거리")]
    //float contactDistance;

    [SerializeField, Header("쫓아가는 타겟(플레이어)")]
    Transform target;

    [SerializeField]
    private Animator animator;

    private Vector3 lastKnownLoc;
    private NavMeshAgent agent;

    void Awake()
    {
        //ani = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        agent.destination = lastKnownLoc = target.position;
        //animator.SetBool("chasing", true);

        /*
        if (isFollow())
        {
            //ani.SetBool("chasing", true);
        }
        //else
        {
            //ani.SetBool("chasing", false);
        }
        */
    }

    public override void OnStateEnter()
    {
        animator.SetBool("IsMove", true);
    }

    public override void OnStateLeave()
    {
        agent.ResetPath();
    }

    public Vector3 GetLastKnownPlayerLocation()
    {
        return lastKnownLoc;
    }
}
