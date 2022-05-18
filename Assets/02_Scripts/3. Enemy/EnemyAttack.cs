using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : FsmState
{
    // �̷��� ������ �ұ� SO�� �ұ�?
    //[SerializeField]
    //private float attackDamage = 1f;
    //[SerializeField]
    //private float attackDelay = 0.5f;

    [SerializeField]
    private EnemyDataSO enemyData = null;

    private int attackLayer = 1 << 9;

    private bool isAttack = false;
    private bool isPlayerDamage = false;

    private float timer = 0f;

    private Collider[] hitColl;
    private Animator ani;

    private FsmCore fsmCore;
    private EnemyIdle chaseState;

    private readonly int attack = Animator.StringToHash("attack");
    private readonly int parrying = Animator.StringToHash("parrying");
    //private readonly int attackCnt = Animator.StringToHash("AttackCount");
    private readonly int isMove = Animator.StringToHash("IsMove");
    private readonly int isIn = Animator.StringToHash("isIn");

    private readonly static WaitForSeconds waitForSeconds05 = new WaitForSeconds(0.5f);

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
        ani.SetBool(isMove, false);
        StopAllCoroutines();
        //Reset();
        timer = enemyData.attackDelay;
    }

    void Update()
    {
        if (!ani.GetBool(parrying))
        {
            hitColl = Physics.OverlapCapsule(transform.position, new Vector3(0, 1.2f, 0), enemyData.attackRange, attackLayer);
            //ani.SetFloat(attackCnt, attackCount);
            ani.SetBool(isIn, hitColl != null);
            //ani.SetBool(attack, isAttack);

            if (!isAttack)
            {
                ani.SetTrigger("Attack");
                AttackChange(1);
                Attack();
            }
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
            foreach (var hitObj in hitColl)
            {
                if (hitObj.CompareTag("Player"))
                {
                    StartCoroutine(AttackCoroutine(hitObj.gameObject));
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
        //fsmCore.ChangeState(chaseState);
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
