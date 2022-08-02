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

    [SerializeField]
    private float transitionTime = 3f; // 틍정 행동이 연달아 (ex 점프 어택 -> 점프 어택) 일어나지 않게 시간 체크 후 전이
    public float TransitionTime => transitionTime;
    private float initTransitionTime;

    private readonly int isMove = Animator.StringToHash("IsMove");
    private readonly string walk = "walk";
    void Awake()
    {
        //ani = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        initTransitionTime = transitionTime;
    }

    void Update()
    {
        //Debug.Log($"플레이어의 거리 : {Vector3.Distance(target.position, transform.position)}");

        if (animator.GetBool(isMove))
        {
            ChangeStop(false);
            transitionTime -= Time.deltaTime;
            if(transitionTime < 0)
            {
                transitionTime = 0f;
            }
            agent.destination = lastKnownLoc = target.position;
            //agent.SetDestination(target.position);
        }
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
        agent.destination = lastKnownLoc = target.position;
        transitionTime = initTransitionTime;
        Debug.Log("Chase 진입");
    }

    public override void OnStateLeave()
    {
        this.GetComponent<Chase>().enabled = false;
        agent.ResetPath();
        Debug.Log("Chase 떠나기");
    }

    public Vector3 GetLastKnownPlayerLocation()
    {
        return lastKnownLoc;
    }
}
