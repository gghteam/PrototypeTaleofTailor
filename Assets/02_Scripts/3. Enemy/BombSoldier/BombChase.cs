using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BombChase : FsmState
{
    [SerializeField, Header("ÂÑ¾Æ°¡´Â Å¸°Ù(ÇÃ·¹ÀÌ¾î)")]
    Transform target;

    private Vector3 lastKnownLoc;
    private NavMeshAgent agent;
    Animation anim;

    bool isChase = false;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animation>();
    }

    void Update()
    {
        if(isChase)
        {
            ChangeStop(false);
            agent.destination = lastKnownLoc = target.position;
        }
        else
            ChangeStop(true);

    }

    public void ChangeStop(bool value)
    {
        agent.isStopped = value;
    }

    public override void OnStateEnter()
    {
        isChase = true;
        anim.CrossFade("Armature_soldier|B_walk");
        agent.destination = lastKnownLoc = target.position;
    }

    public override void OnStateLeave()
    {
        isChase = false;
        agent.ResetPath();
        this.GetComponent<BombChase>().enabled = false;
    }

    public Vector3 GetLastKnownPlayerLocation()
    {
        return lastKnownLoc;
    }

}
