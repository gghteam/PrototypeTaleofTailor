using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootCondition : FsmState
{
    private FsmLegacyAni fsmLegacyAni;

    private void Awake()
    {
        fsmLegacyAni = GetComponent<FsmLegacyAni>();
    }

    public override void OnStateEnter()
    {
        Debug.Log("Shoot");
        fsmLegacyAni.ChangeAnimation(FsmLegacyAni.ClipState.Attack, 0.3f);
    }
}
