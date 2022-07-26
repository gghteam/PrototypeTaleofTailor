using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class ChaseRange : FsmCondition
{
    public float Range;

    private EnemyAttack enemyAttack;
    private FsmCore fsmCore;

    private void Start()
    {
        enemyAttack = GetComponent<EnemyAttack>();
        fsmCore = GetComponent<FsmCore>();
    }

    public override bool IsSatisfied(FsmState curr, FsmState next)
    {
        if(fsmCore.GetCurrentState().State == enemyAttack)
        {
            return (Vector3.Distance(transform.position, Player.transform.position) > Range) && enemyAttack.IsAttack == false;
        }
        else
        {
            return (Vector3.Distance(transform.position, Player.transform.position) > Range);
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, Range);
        Gizmos.color = Color.white;
    }
#endif
}
