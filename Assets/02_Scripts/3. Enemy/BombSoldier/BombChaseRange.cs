using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombChaseRange : FsmCondition
{
    public float Range;
    public float maxRange;

    public Transform Player;

    private bool isAttack = false;

    private void Start()
    {
        EventManager.StartListening("BombStart", IsBomb);


    }
    private void OnDestroy()
    {
        EventManager.StopListening("BombStart", IsBomb);
    }

    public override bool IsSatisfied(FsmState curr, FsmState next)
    {
        float dis = Vector3.Distance(transform.position, Player.position);
        if (dis > Range && dis < maxRange)
        {
            if (isAttack) return false;
            else return true;
        }
        else return false;
    }

    private void IsBomb(EventParam eventParam)
    {
        isAttack = eventParam.boolParam;
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, Range);
        Gizmos.DrawWireSphere(transform.position, maxRange);
        Gizmos.color = Color.white;
    }
#endif
}
