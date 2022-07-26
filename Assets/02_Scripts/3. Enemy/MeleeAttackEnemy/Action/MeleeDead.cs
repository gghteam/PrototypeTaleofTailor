using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeDead : FsmState
{
    private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public override void OnStateEnter()
    {
        agent.isStopped = true;
        // 사망 처리하기
        // 사망 이펙트
        // 이펙트 종류후 콜라이더 or 엑티브 끄기
    }

    public override void OnStateLeave()
    {

    }
}
