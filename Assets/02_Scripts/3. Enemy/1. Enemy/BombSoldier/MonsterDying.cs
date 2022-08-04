using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterDying : FsmState
{
    private new Collider collider;

    FsmLegacyAni anim;

    private void Awake()
    {
        anim = GetComponent<FsmLegacyAni>();
        collider = GetComponent<Collider>();
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
        collider.enabled = false;

        anim.ChangeAnimation(FsmLegacyAni.ClipState.Dead, 0.25f);
        //Invoke("Die", 2f);
    }

    void Die()
    {
        gameObject.SetActive(false);
    }


}
