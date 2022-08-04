using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterDying : FsmState
{
    FsmLegacyAni anim;

    private void Awake()
    {
        anim = GetComponent<FsmLegacyAni>();
    }

    public override void OnStateEnter()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        if (agent != null)
            agent.enabled = false;
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        if (rigidbody != null)
        {
            rigidbody.useGravity = false;
            rigidbody.freezeRotation = true;
        }

        anim.ChangeAnimation(FsmLegacyAni.ClipState.Dead, 0.25f);
        //Invoke("Die", 2f);
    }

    void Die()
    {
        gameObject.SetActive(false);
    }


}
