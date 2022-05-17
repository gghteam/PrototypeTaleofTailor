using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdle : FsmState
{
    int random = 0;
    float time = 0;

    private FsmCore fsmCore;
    private Chase chaseState;

    private void Start()
    {
        fsmCore = GetComponent<FsmCore>();
        chaseState = GetComponent<Chase>();
    }
    public override void OnStateEnter()
    {
        random = Random.Range(3, 5);
        time = 0;
    }

    private void Update()
    {
        time += Time.deltaTime;

        if(time >= random)
        {
            fsmCore.ChangeState(chaseState);
        }
    }
}
