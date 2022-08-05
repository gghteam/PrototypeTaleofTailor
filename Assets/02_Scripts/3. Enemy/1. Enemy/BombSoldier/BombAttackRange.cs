using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombAttackRange : FsmCondition
{
    public float Range;

    public Transform Player;


    public override bool IsSatisfied(FsmState curr, FsmState next)
    {
        return Vector3.Distance(transform.position, Player.position) < Range;
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, Range);
        Gizmos.color = Color.white;
    }
#endif
}
