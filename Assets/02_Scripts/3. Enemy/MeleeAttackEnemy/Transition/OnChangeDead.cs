using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnChangeDead : FsmCondition
{
    public override bool IsSatisfied(FsmState curr, FsmState next)
    {
        // �� ���� HP�� 0 �����϶�
        return true;
    }
}
