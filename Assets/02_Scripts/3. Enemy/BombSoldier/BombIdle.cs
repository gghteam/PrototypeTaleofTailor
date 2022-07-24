using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BombIdle : FsmState
{
    FsmLegacyAni anim;

    private void Awake()
    {
        anim = GetComponent<FsmLegacyAni>();
    }

    public override void OnStateEnter()
    {
        anim.ChangeAnimation(FsmLegacyAni.ClipState.Idle, 0.25f);
    }
    public override void OnStateLeave()
    {
        this.GetComponent<BombIdle>().enabled = false;
    }

}
