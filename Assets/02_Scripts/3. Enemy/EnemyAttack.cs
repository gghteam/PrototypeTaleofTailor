using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyAttack : FsmState
{
    // �̷��� ������ �ұ� SO�� �ұ�?
    //[SerializeField]
    //private float attackDamage = 1f;
    //[SerializeField]
    //private float attackDelay = 0.5f;

    [SerializeField]
    private EnemyDataSO enemyData = null;

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

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position + new Vector3(0, 1.2f, 0), enemyData.attackRange);
    }
#endif

    void Update()
    {
        if (isPlay)
        {
            hitColl = Physics.OverlapCapsule(transform.position, new Vector3(0, 1.2f, 0), enemyData.attackRange, attackLayer).FirstOrDefault();
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
                    Attack();
                }
            }
        }

        if (!isPlay)
        {
            fsmCore.ChangeState(chaseState);
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
    /// �и������� �� �Լ�
    /// </summary>
    private void ParryingAction()
    {
        Debug.Log("�� ġ�פ�");
    }

    /// <summary>
    /// ���� �Լ�
    /// </summary>
    private void Attack()
    {
        //attackCount = (attackCount + 1) % 2;
        //AttackChange(1);
        // Debug.Log("ENEMY ATTACK");

        if (!isAttack)
        {
            AttackChange(1);
            if (hitColl != null)
            {
                if (hitColl.CompareTag("Player"))
                {
                    StartCoroutine(AttackCoroutine(hitColl.gameObject));
                }
            }
        }
        //Debug.Log("SPeed");
    }

    /// <summary>
    /// ���� �ڷ�ƾ
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
            }
        }
        isPlay = false;
        fsmCore.ChangeState(chaseState);
    }

    /// <summary>
    /// �ִϸ����;ȿ� �ִ� parrying�Ķ���͸� ��ȭ��Ű�� �Լ�
    /// </summary>
    /// <param name="value"></param>
    private void ParryingChange(int value)
    {
        ani.SetBool(parrying, value != 0);
    }

    /// <summary>
    /// isAttack����(���� ����������?)�� �����Ű�� �Լ�
    /// </summary>
    /// <param name="value"></param>
    private void AttackChange(int value)
    {
        isAttack = value != 0;
    }

    /// <summary>
    /// isPlayerDamage����(�÷��̾ �������� �޴� ������?)�� �����Ű�� �Լ�. ��Ʈ���� ���� �뵵
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
}
