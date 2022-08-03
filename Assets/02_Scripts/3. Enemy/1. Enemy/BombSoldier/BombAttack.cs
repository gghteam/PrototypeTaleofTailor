using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombAttack : FsmState
{
    [SerializeField, Header("���� �ӵ�")]
    float attackSec;
    
    [SerializeField, Header("��ǥ ����")]
    Transform target;
    [SerializeField, Header("�Ѿ� ���� ��ġ")]
    Transform bombPos;

    [SerializeField, Header("��ź Prefab")]
    GameObject bomb;

    FsmLegacyAni anim;
    EventParam eventParam;

    private void Awake()
    {
        anim = GetComponent<FsmLegacyAni>();
    }
    private void Update()
    {
        LookPlayer();
    }

    public override void OnStateEnter()
    {
        eventParam.boolParam = true;
        EventManager.TriggerEvent("BombStart", eventParam);

        InvokeRepeating("Attack", 0.1f, attackSec);
    }
    public override void OnStateLeave()
    {
        CancelInvoke();
        //this.GetComponent<BombAttack>().enabled = false;
    }

    // ��ź ����
    void Attack()
    {
        anim.ChangeAnimation(FsmLegacyAni.ClipState.Reloading, 0.25f);
        Invoke("Shoot", 3.5f);
    }

    void Shoot()
    {
        anim.ChangeAnimation(FsmLegacyAni.ClipState.Attack, 0.25f);
        //GameObject _bullet = Instantiate(bomb, bombPos.position, transform.rotation);
        BombBullet _bullet = PoolManager.Instance.Pop("Bomb") as BombBullet;
        _bullet.transform.SetPositionAndRotation(bombPos.position, GetDirection());
        
        eventParam.boolParam = false;
        EventManager.TriggerEvent("BombStart", eventParam);
    }

    void LookPlayer()
    {
        Vector3 vec = target.position-transform.position;
        vec.Normalize();
        Quaternion q = Quaternion.LookRotation(vec);
        transform.rotation = q;
    }

    private Quaternion GetDirection()
    {
        Vector3 vec = target.position - transform.position;
        vec.Normalize();
        Quaternion q = Quaternion.LookRotation(vec);
        return q;
    }
}
