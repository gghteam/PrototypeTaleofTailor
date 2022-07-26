using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BombIdle : FsmState
{
    Animation anim;

    private void Awake()
    {
        anim = GetComponent<Animation>();
    }

    public override void OnStateEnter()
    {
        anim.CrossFade("Armature_soldier|B_watches");
    }
    public override void OnStateLeave()
    {
        this.GetComponent<BombIdle>().enabled = false;
    }

}
