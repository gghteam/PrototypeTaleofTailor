using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Enemy")]
public class EnemyDataSO : ScriptableObject
{
    public float attackDamage;
    public float attackDelay;
    public float attackRange;
    [Range(0f, 360f)]
    public float viewAngle = 60f;
}
