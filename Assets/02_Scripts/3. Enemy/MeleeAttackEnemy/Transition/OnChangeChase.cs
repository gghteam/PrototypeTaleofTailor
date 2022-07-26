using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class OnChangeChase : FsmCondition
{
    [SerializeField]
    private float chaseRange;

    private MeleeAttack attack;

    private void Start()
    {
        attack = GetComponent<MeleeAttack>();
    }

    public override bool IsSatisfied(FsmState curr, FsmState next)
    {
        bool distance = Vector3.Distance(transform.position, Player.transform.position) < chaseRange;
        bool isAttack = attack.IsAttack;
        return distance == true;
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
        Gizmos.color = Color.white;
    }
#endif
}
