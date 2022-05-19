using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdle : FsmState
{
    int random = 0;
    float time = 0;

    private FsmCore fsmCore;
    private Chase chaseState;

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
    }
    public override void OnStateEnter()
    {
    }

    private void Update()
    {

        if(isFollow() == true)
        {
            if (UIManager.Instance.IsSound) return;
            canvas.SetActive(true);
            soundSet.SrartBGM();
            fsmCore.ChangeState(chaseState);
            UIManager.Instance.IsSound = true;
        }
    }

    bool isFollow()
    {
        return Vector3.Distance(transform.position, target.position) < contactDistance;
    }
}
