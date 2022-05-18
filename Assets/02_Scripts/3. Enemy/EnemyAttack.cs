using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : FsmState
{
    // 이렇게 변수로 할까 SO로 할까?
    //[SerializeField]
    //private float attackDamage = 1f;
    //[SerializeField]
    //private float attackDelay = 0.5f;

    [SerializeField]
    private EnemyDataSO enemyData = null;

    private int attackLayer = 1 << 6;

    private bool isAttack = false;
    private bool isPlayerDamage = false;

    private int attackCount = 0;

    private float timer = 0f;

    private Collider[] hitColl;
    private Animator ani;

    private FsmCore fsmCore;
    private EnemyIdle chaseState;

    private readonly int attack = Animator.StringToHash("attack");
    private readonly int parrying = Animator.StringToHash("parrying");
    private readonly int attackCnt = Animator.StringToHash("AttackCount");

    EventParam eventParam;
    void Start()
    {
        ani = GetComponent<Animator>();
        fsmCore = GetComponent<FsmCore>();
        chaseState = GetComponent<EnemyIdle>();
        Reset();

        timer = enemyData.attackDelay;
    }


    public override void OnStateEnter()
    {
        ani.SetBool("IsMove", false);
        StopAllCoroutines();
        Reset();
        timer = enemyData.attackDelay;
    }

    void Update()
    {
        if (!ani.GetBool(parrying))
        {
            hitColl = Physics.OverlapCapsule(transform.position, new Vector3(0, 2.2f, 0), enemyData.attackRange, attackLayer);

            ani.SetFloat(attackCnt, attackCount);

            Attack();
        }

        /*
        if (timer >= enemyData.attackDelay)
        {
            if (!isAttack)
            {
                Attack();
                timer = 0;
                fsmCore.ChangeState(chaseState);
            }
        }
        */
    }

    /// <summary>
    /// 패링당했을 때 함수
    /// </summary>
    private void ParryingAction()
    {
        Debug.Log("쫌 치네ㅋ");
    }

    /// <summary>
    /// 공격 함수
    /// </summary>
    private void Attack()
    {
        attackCount = (attackCount + 1) % 2;
        AttackChange(1);
       // Debug.Log("ENEMY ATTACK");
        ani.SetTrigger(attack);

        if (isAttack)
        {
            foreach(var hitObj in hitColl)
            {
                if (hitObj.CompareTag("Player"))
                {
                    StartCoroutine(AttackCoroutine(hitObj.gameObject));
                }
            }
        }
        Debug.Log("SPeed");
    }

    /// <summary>
    /// 공격 코루틴
    /// </summary>
    /// <returns></returns>
    private IEnumerator AttackCoroutine(GameObject hitObj)
    {
        yield return new WaitForSeconds(.5f);

        if (!isPlayerDamage)
        {
            PlayerParrying player = hitObj.GetComponent<PlayerParrying>();
            bool isParrying = player.IsParrying;

            if (isParrying)
            {
                if (player.IsInViewangle(this.transform))
                {
                    player.SuccessParrying();
                    ParryingChange(1);
                    ParryingAction();
                }
                else
                {
                    player.FailedParrying();
                    ParryingChange(0);
                    eventParam.intParam = 200;
                    eventParam.stringParam = "PLAYER";
                    EventManager.TriggerEvent("DAMAGE", eventParam);
                    PlayerDamageChange(1);
                }
            }
            else
            {
                player.FailedParrying();
                ParryingChange(0);
                eventParam.intParam = 200;
                eventParam.stringParam = "PLAYER";
                EventManager.TriggerEvent("DAMAGE", eventParam);
                PlayerDamageChange(1);
;            }
        }
        fsmCore.ChangeState(chaseState);
    }

    /// <summary>
    /// 애니메이터안에 있는 parrying파라매터를 변화시키는 함수
    /// </summary>
    /// <param name="value"></param>
    private void ParryingChange(int value)
    {
        ani.SetBool(parrying, value == 0 ? false : true);
    }

    /// <summary>
    /// isAttack변수(현재 공격중인지?)를 변경시키는 함수
    /// </summary>
    /// <param name="value"></param>
    private void AttackChange(int value)
    {
        isAttack = value == 0 ? false : true;
    }

    /// <summary>
    /// isPlayerDamage변수(플레이어가 데미지를 받는 중인지?)를 변경시키는 함수. 도트뎀을 막는 용도
    /// </summary>
    /// <param name="value"></param>
    private void PlayerDamageChange(int value)
    {
        isPlayerDamage = value == 0 ? false : true;
    }

    public void Reset()
    {
        PlayerDamageChange(0);
        AttackChange(0);
        ParryingChange(0);
    }
}
