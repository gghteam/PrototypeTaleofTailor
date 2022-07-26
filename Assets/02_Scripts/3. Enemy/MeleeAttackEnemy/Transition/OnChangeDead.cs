using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnChangeDead : FsmCondition
{
    public override bool IsSatisfied(FsmState curr, FsmState next)
    {
        // 이 적의 HP가 0 이하일때
        return true;
    }
}
