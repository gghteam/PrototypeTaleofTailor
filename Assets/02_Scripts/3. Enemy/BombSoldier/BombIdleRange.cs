using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombIdleRange : FsmCondition
{
    public float Range;
    public float maxRange;

    public Transform Player;

    public override bool IsSatisfied(FsmState curr, FsmState next)
    {
        return Vector3.Distance(transform.position, Player.position) > Range;
    }
}
