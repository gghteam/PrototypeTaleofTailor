using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Enemies/AttackData")]
public class EnemyAttackDataSO : ScriptableObject
{
    public float attackDelay = 1f;
    public bool isAttackDelayRandomness = false;
    public float attackDelayRandomness = 1f;
    public float attackRange = 3f;
    [Range(0f, 180f)]
    public float viewAngle = 60;
    public int attackDamage = 30;
}
