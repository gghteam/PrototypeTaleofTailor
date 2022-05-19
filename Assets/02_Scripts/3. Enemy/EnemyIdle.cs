using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdle : FsmState
{
    int random = 0;
    float time = 0;

    private FsmCore fsmCore;
    private Chase chaseState;
    private Animator ani;

    private readonly int isMove = Animator.StringToHash("IsMove");

    [SerializeField, Header("근접 거리")]
    float contactDistance;

    [SerializeField, Header("쫓아가는 타겟(플레이어)")]
    Transform target;

    [SerializeField]
    private GameObject canvas;

    [SerializeField]
    private SoundSet soundSet;

    private void Start()
    {
        fsmCore = GetComponent<FsmCore>();
        chaseState = GetComponent<Chase>();
        ani = GetComponent<Animator>();
    }
    public override void OnStateEnter()
    {
        if (ani == null)
            ani = GetComponent<Animator>();
        ani.SetBool(isMove, false);
        Debug.Log("IDLE 상태");
    }

    private void Update()
    {
        if(isFollow() == true)
        {
            canvas.SetActive(true);
            soundSet.SrartBGM();
            fsmCore.ChangeState(chaseState);
        }
    }

    bool isFollow()
    {
        return Vector3.Distance(transform.position, target.position) < contactDistance;
    }
}
