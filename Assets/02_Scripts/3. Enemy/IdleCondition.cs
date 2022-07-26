using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleCondition : FsmState
{
    private FsmLegacyAni fsmLegacyAni;


    private void Awake()
    {
        fsmLegacyAni = GetComponent<FsmLegacyAni>();
    }
    public override void OnStateEnter()
    {
        fsmLegacyAni.ChangeAnimation(FsmLegacyAni.ClipState.Idle, 0.3f);
    }
}
