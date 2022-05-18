using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chase : FsmState
{
    //[SerializeField, Header("�߰� �ӵ�")]
    //float chaseSpeed;

    //[SerializeField, Header("���� �Ÿ�")]
    //float contactDistance;

    [SerializeField, Header("�Ѿư��� Ÿ��(�÷��̾�)")]
    Transform target;

    [SerializeField]
    private Animator animator;

    private Vector3 lastKnownLoc;
    private NavMeshAgent agent;

    private readonly int isMove = Animator.StringToHash("IsMove");
    private readonly string walk = "walk";
    void Awake()
    {
        //ani = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        agent.destination = lastKnownLoc = target.position;

        if (animator.GetCurrentAnimatorStateInfo(0).IsName(walk))
            ChangeStop(false);
        else
            ChangeStop(true);

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

    public void ChangeStop(bool value)
    {
        agent.isStopped = value;
    }

    public override void OnStateEnter()
    {
        animator.SetBool(isMove, true);
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
