using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static Define;

public class MeleeChase : FsmState
{
    private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public override void OnStateEnter()
    {
        // ���ϸ��̼� ��ü(Walk)
    }

    public override void OnStateLeave()
    {
        
    }

    private void Update()
    {
        agent.SetDestination(Player.transform.position);
    }
}
