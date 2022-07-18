using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Enemies/AttackData")]
public class EnemyAttackDataSO : ScriptableObject
{
    public float attackDelay = 1f;
    public float attackRange = 3f;
    [Range(0f, 180f)]
    public float viewAngle = 60;
    [Range(1f, 100f)]
    public float attackDamage = 30f;
}
