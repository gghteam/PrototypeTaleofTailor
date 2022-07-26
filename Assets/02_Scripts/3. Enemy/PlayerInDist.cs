using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInDist : FsmCondition
{
    public float dist;
    public Color distColor;

    public Transform targetTransform;
    public FsmState compareState;

    private FsmState checkState;

    private void OnDrawGizmos()
    {
        if (checkState == compareState)
        {
            Gizmos.color = distColor;
            Gizmos.DrawWireSphere(transform.position, dist);
        }
    }

    public override bool IsSatisfied(FsmState curr, FsmState next)
    {
        checkState = curr;
        float distance = Vector3.Distance(transform.position, targetTransform.position);

        return distance <= dist;
    }
}
