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
        // ��� ó���ϱ�
        // ��� ����Ʈ
        // ����Ʈ ������ �ݶ��̴� or ��Ƽ�� ����
    }

    public override void OnStateLeave()
    {

    }
}
