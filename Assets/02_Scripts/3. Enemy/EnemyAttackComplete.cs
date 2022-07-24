using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class EnemyAttackComplete : FsmCondition
{
    public float Range;

    private EnemyAttack enemyAttack;

    private void Start()
    {
        enemyAttack = GetComponent<EnemyAttack>();
    }

    public override bool IsSatisfied(FsmState curr, FsmState next)
    {
        return (Vector3.Distance(Player.transform.position, transform.position) > Range) && enemyAttack.IsAttack == false;
    }
}
