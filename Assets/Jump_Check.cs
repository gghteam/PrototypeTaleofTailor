using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump_Check : FsmCondition
{
    public override bool IsSatisfied(FsmState curr, FsmState next)
    {
        Debug.Log("³ª°¨");
        return true;
    }
}
