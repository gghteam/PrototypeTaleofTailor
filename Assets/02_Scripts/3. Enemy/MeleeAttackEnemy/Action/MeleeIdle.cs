using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeIdle : FsmState
{
    // 후에 점더 발전시키기
    // 현재 : 일단 정지
    // 목표 : 이 적의 월래 있어야 할 곳으로 돌아가기

    private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public override void OnStateEnter()
    {
        agent.isStopped = true;
        // 애니메이션 처리(Idle)
    }

    public override void OnStateLeave()
    {
        agent.isStopped = false;
    }
}
