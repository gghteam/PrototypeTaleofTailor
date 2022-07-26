using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeIdle : FsmState
{
    // �Ŀ� ���� ������Ű��
    // ���� : �ϴ� ����
    // ��ǥ : �� ���� ���� �־�� �� ������ ���ư���

    private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public override void OnStateEnter()
    {
        agent.isStopped = true;
        // �ִϸ��̼� ó��(Idle)
    }

    public override void OnStateLeave()
    {
        agent.isStopped = false;
    }
}
