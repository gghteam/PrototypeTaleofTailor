using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyAttack : FsmState
{
    [SerializeField]
    private float attackDelay = 1f;
    [SerializeField]
    private float attackRange = 3f;
    private float viewAngle = 60f;

    [SerializeField]
    private Collider[] colliders;

    private int attackLayer = 1 << 10;

    private bool isAttack = false;
    private bool isPlayerDamage = false;

    private float timer = 0f;
    private int attackCnt = 0;

    private Collider[] hitColls;
    private Collider hitColl;
    private Animator ani;

    //private FsmCore fsmCore;
    //private EnemyIdle chaseState;

    private readonly int parrying = Animator.StringToHash("parrying");
    private readonly int isMove = Animator.StringToHash("IsMove");
    //private readonly int isIn = Animator.StringToHash("isIn");
    //private readonly int attackTrigger = Animator.StringToHash("Attack");
    private readonly int attack = Animator.StringToHash("Attack");
    private readonly int attackCount = Animator.StringToHash("count");

    private readonly static WaitForSeconds waitForSeconds05 = new WaitForSeconds(0.5f);

    EventParam eventParam;
    void Start()
    {
        ani = GetComponent<Animator>();
        //fsmCore = GetComponent<FsmCore>();
        //chaseState = GetComponent<EnemyIdle>();
        //Reset();

        timer = attackDelay;
    }

    public override void OnStateEnter()
    {
        timer = attackDelay;
        ani.SetBool(isMove, false);
        ani.SetBool(parrying, false);
        ani.SetBool(attack, false);
        StopAllCoroutines();
    }

    public override void OnStateLeave()
    {
        StopAllCoroutines();
        ani.SetBool(isMove, true);
        ani.SetBool(attack, false);
        Reset();
        attackCnt = 0;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position + new Vector3(0, 2, 0), attackRange);
    }

    void Update()
    {
        timer += Time.deltaTime;
        hitColls = Physics.OverlapCapsule(transform.position, new Vector3(0, 1.2f, 0), attackRange, attackLayer);
        hitColl = hitColls.Where(coll => coll.CompareTag("Player")).FirstOrDefault();
        ani.SetBool(attack, isAttack);
        ani.SetInteger(attackCount, attackCnt % 2);

        //foreach (Collider coll in hitColls) // �� Linq�� ���� ���
        //{
        //    if (coll.CompareTag("Player"))
        //    {
        //        hitColl = coll;
        //    }
        //}

        if (hitColl != null)
        {
            transform.LookAt(hitColl.transform);

            Debug.Log(hitColl);
            if (timer >= attackDelay)
            {
                ++attackCnt;
                Debug.Log("����!");
                AttackChange(1);
                timer = 0f;
            }
        }
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
    /// �и������� �� �Լ�
    /// </summary>
    private void ParryingAction()
    {
        Debug.Log("�� ġ�פ�");
    }

    /// <summary>
    /// ���� �Լ�
    /// </summary>
    [System.Obsolete]
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
    /// ���� �ڷ�ƾ
    /// </summary>
    /// <returns></returns>
    [System.Obsolete]
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
            Debug.Log("�÷��̾����� ����");
            if (!isPlayerDamage)
            {
                Debug.Log("������ ���� ó��");
                PlayerDamageChange(1);
                Damage();
            }
        }
    }
}
