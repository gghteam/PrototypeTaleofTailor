using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class OnChangeAttack : FsmCondition
{
    [SerializeField]
    private float attackRange;

    public override bool IsSatisfied(FsmState curr, FsmState next)
    {
        return Vector3.Distance(transform.position, Player.transform.position) < attackRange;
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.white;
    }
#endif
}
