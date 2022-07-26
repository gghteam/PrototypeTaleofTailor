using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombKilled : FsmCondition
{
    private bool dead;

    public bool Dead
    {
        get { return dead; }
        set { dead = value; }
    }

    private void Update()
    {
        if (dead)
        {
            Debug.Log("dead == true");
        }
    }

    public override bool IsSatisfied(FsmState curr, FsmState next)
    {
        return dead && !(curr is BombDying);
    }
}
