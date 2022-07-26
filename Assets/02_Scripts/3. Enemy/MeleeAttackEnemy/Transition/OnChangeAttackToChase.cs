using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class OnChangeAttackToChase : FsmCondition
{
    [SerializeField]
    private float range;

    public override bool IsSatisfied(FsmState curr, FsmState next)
    {
        bool distance = Vector3.Distance(transform.position, Player.transform.position) > range;
        MeleeAttack attack = curr as MeleeAttack;
        bool isAttack = attack.IsAttack;
        return distance == true && isAttack == false;
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, range);
        Gizmos.color = Color.white;
    }
#endif
}
