using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyAttack : FsmState
{
    // 이렇게 변수로 할까 SO로 할까?
    //[SerializeField]
    //private float attackDamage = 1f;
    //[SerializeField]
    //private float attackDelay = 0.5f;

    [SerializeField]
    private EnemyDataSO enemyData = null;

    [SerializeField]
    private Collider[] colliders;

    private int attackLayer = 1 << 10;

    private bool isAttack = false;
    private bool isPlayerDamage = false;
    private bool isPlay = false;

    private float timer = 0f;

    private Collider hitColl;
    private Animator ani;

    private FsmCore fsmCore;
    private EnemyIdle chaseState;

    private readonly int parrying = Animator.StringToHash("parrying");
    private readonly int isMove = Animator.StringToHash("IsMove");
    //private readonly int isIn = Animator.StringToHash("isIn");
    //private readonly int attackTrigger = Animator.StringToHash("Attack");
    private readonly int attack = Animator.StringToHash("Attack");

    private readonly static WaitForSeconds waitForSeconds05 = new WaitForSeconds(0.5f);

    EventParam eventParam;
    void Start()
    {
        ani = GetComponent<Animator>();
        fsmCore = GetComponent<FsmCore>();
        chaseState = GetComponent<EnemyIdle>();
        //Reset();

        timer = enemyData.attackDelay;
    }

    public override void OnStateEnter()
    {
        this.GetComponent<EnemyIdle>().enabled = false;
        this.GetComponent<Chase>().enabled = false;
        isAttack = false;
        ani.SetBool(isMove, false);
        ani.SetBool(parrying, false);
        ani.SetBool(attack, true);
        //ani.SetTrigger(attackTrigger);
        StopAllCoroutines();
        //Reset();
        timer = enemyData.attackDelay;
        isPlay = true;
    }

    public override void OnStateLeave()
    {
        StopAllCoroutines();
        isPlay = false;
        ani.SetBool(isMove, true);
        ani.SetBool(attack, false);
        this.GetComponent<EnemyAttack>().enabled = false;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position + new Vector3(0, 2, 0), enemyData.attackRange);
    }

    void Update()
    {
        if (isPlay)
        {
            hitColl = Physics.OverlapCapsule(transform.position, new Vector3(0, 1.2f, 0), enemyData.attackRange, attackLayer).FirstOrDefault();
            Debug.Log(hitColl);
            //ani.SetBool(isIn, hitColl != null);
            timer += Time.deltaTime;
            //ani.SetBool(attack, isAttack);
            if (!ani.GetBool(parrying))
            {
                //ani.SetFloat(attackCnt, attackCount);
                //ani.SetBool(attack, isAttack);

                //if (!ani.GetBool(isIn))
                //{
                //    fsmCore.ChangeState(chaseState);
                //}

                if (timer >= enemyData.attackDelay)
                {
                    //if (!isAttack)
                    //{
                    //    //ani.SetTrigger(attackTrigger);
                    //    //AttackChange(1);
                    //    Attack();
                    //}
                    //Attack();
                }
            }
        }

        if (!isPlay)
        {
            fsmCore.ChangeState(chaseState);
            this.GetComponent<EnemyAttack>().enabled = false;
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

    private void Damage()
    {
        if(hitColl != null)
        {
            if (hitColl.CompareTag("Player"))
            {
                PlayerParrying player = hitColl.GetComponent<PlayerParrying>();
                bool isParrying = player.IsParrying;
                if (isParrying)
                {
                    ParryingAction();
                    player.SuccessParrying();
                    ani.SetTrigger(parrying);
                }
                else
                {
                    eventParam.intParam = 30;
                    eventParam.stringParam = "PLAYER";
                    EventManager.TriggerEvent("DAMAGE", eventParam);
                }
                PlayerDamageChange(0);
            }
        }
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
        //attackCount = (attackCount + 1) % 2;
        //AttackChange(1);
        // Debug.Log("ENEMY ATTACK");

        //if (!isAttack)
        //{
        //    AttackChange(1);
        //    if (hitColl != null)
        //    {
        //        if (hitColl.CompareTag("Player"))
        //        {
        //            StartCoroutine(AttackCoroutine(hitColl.gameObject));
        //        }
        //    }
        //}

        if (hitColl != null)
        {
            if (hitColl.CompareTag("Player"))
            {
                StartCoroutine(AttackCoroutine(hitColl.gameObject));
            }
        }
        //Debug.Log("SPeed");
    }

    /// <summary>
    /// 공격 코루틴
    /// </summary>
    /// <returns></returns>
    private IEnumerator AttackCoroutine(GameObject hitObj)
    {
        yield return waitForSeconds05;

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
                    //eventParam.intParam = 200;
                    //eventParam.stringParam = "PLAYER";
                    //EventManager.TriggerEvent("DAMAGE", eventParam);
                    PlayerDamageChange(1);
                }
            }
            else
            {
                player.FailedParrying();
                ParryingChange(0);
                //eventParam.intParam = 200;
                //eventParam.stringParam = "PLAYER";
                //EventManager.TriggerEvent("DAMAGE", eventParam);
                PlayerDamageChange(1);
            }
        }
        isPlay = false;
        fsmCore.ChangeState(chaseState);
    }

    /// <summary>
    /// 애니메이터안에 있는 parrying파라매터를 변화시키는 함수
    /// </summary>
    /// <param name="value"></param>
    private void ParryingChange(int value)
    {
        ani.SetBool(parrying, value != 0);
    }

    /// <summary>
    /// isAttack변수(현재 공격중인지?)를 변경시키는 함수
    /// </summary>
    /// <param name="value"></param>
    private void AttackChange(int value)
    {
        isAttack = value != 0;
    }

    /// <summary>
    /// isPlayerDamage변수(플레이어가 데미지를 받는 중인지?)를 변경시키는 함수. 도트뎀을 막는 용도
    /// </summary>
    /// <param name="value"></param>
    private void PlayerDamageChange(int value)
    {
        isPlayerDamage = value != 0;
    }

    public void Reset()
    {
        PlayerDamageChange(0);
        AttackChange(0);
        ParryingChange(0);
    }

    private void ColliderEnabledChange(int value)
    {
        foreach(Collider coll in colliders)
        {
            coll.enabled = value != 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        if (other.CompareTag("Player"))
        {
            Debug.Log("플레이어한테 닿음");
            if (!isPlayerDamage)
            {
                Debug.Log("데미지 정상 처리");
                PlayerDamageChange(1);
                Damage();
            }
        }
    }
}
