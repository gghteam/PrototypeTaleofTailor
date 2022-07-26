using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : EnemyAttack
{
    private EnemyAttackDataSO attackData;

    private float attackTimer = 0f;

    public override void OnStateEnter()
    {
        attackTimer = attackData.attackDelay;
    }

    public override void OnStateLeave()
    {
        isAttack = false;
    }
}
