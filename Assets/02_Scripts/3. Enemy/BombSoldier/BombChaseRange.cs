using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombChaseRange : FsmCondition
{
    public float Range;
    public float maxRange;

    public Transform Player;

    public override bool IsSatisfied(FsmState curr, FsmState next)
    {
        float dis = Vector3.Distance(transform.position, Player.position);
        return  dis > Range && dis < maxRange;
    }
}
