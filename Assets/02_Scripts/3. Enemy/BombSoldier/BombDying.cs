using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BombDying : FsmState
{
    Animation anim;

    private void Awake()
    {
        anim = GetComponent<Animation>();
    }

    public override void OnStateEnter()
    {
        GetComponent<BombAttack>().enabled = false;
        GetComponent<BombIdle>().enabled = false;
        GetComponent<BombChase>().enabled = false;
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        if (agent != null)
            agent.enabled = false;

        anim.CrossFade("Armature_soldier_B_dead", 0.25f);
        Invoke("Die", 2f);
    }

    void Die()
    {
        gameObject.SetActive(false);
    }


}
