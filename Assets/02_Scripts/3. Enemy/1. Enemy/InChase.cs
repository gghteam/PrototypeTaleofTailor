using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InChase : FsmCondition
{
    public float Range;

    private GameObject targetObject;

    void Start()
    {
        targetObject = GameObject.FindWithTag("Player");
    }
    public override bool IsSatisfied(FsmState curr, FsmState next)
    {
        return Vector3.Distance(transform.position,targetObject.transform.position) <= Range;
    }
}
