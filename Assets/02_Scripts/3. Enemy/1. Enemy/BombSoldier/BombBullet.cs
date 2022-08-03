using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBullet : PoolableMono
{
    [SerializeField, Header("������ ������")]
    int damage = 50;
    [SerializeField, Header("������ �ð�")]
    float bombSec;
    [SerializeField, Header("�ӵ�")]
    float bombPower;

    [SerializeField, Header("����")]
    float radius;
    [SerializeField, Header("���� ��ƼŬ")]
    GameObject bombParticle;

    Rigidbody rb;
    EventParam eventParam;

    bool isDamaged = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        isDamaged = false;
        Invoke("BoomDamage", bombSec);
        //rb.AddForce(transform.forward * bombPower, ForceMode.Impulse);
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * bombPower * Time.deltaTime);
    }


    // ������ �ð��� �ٴ޾��� �� ���� �÷��̾ �ٸ� ������Ʈ�� ����� ���� üũ�ؾ��Ѵ�.
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("ENEMY") == false) // �΋H�Ѱ� ���� �ƴϴ� ��
        {
            BoomDamage();
        }
    }

    // ���� üũ
    void BoomDamage()
    {
        //rb.angularVelocity = Vector3.zero;
        rb.velocity = Vector3.zero;
        var hits = Physics.SphereCastAll(transform.position, radius, Vector3.up, 0f, LayerMask.GetMask("Controller"));
        foreach(var hit in hits)
        {
            // ��ź ���� ��
            if (hit.collider.CompareTag("Player"))
            {
                if (isDamaged) return;
                isDamaged = true;
                eventParam.stringParam = "PLAYER";
                eventParam.intParam = damage;
                EventManager.TriggerEvent("DAMAGE", eventParam);
                EndBoom();
            }
        }
    }


    // ��ź�� ������
    void EndBoom()
    {
        //Instantiate(bombParticle, transform.position, Quaternion.identity);
        ParticlePool particle = PoolManager.Instance.Pop("BombParticle") as ParticlePool;
        particle.transform.position = transform.position;
        //gameObject.SetActive(false);
        PoolManager.Instance.Push(this);
    }

    public override void Reset()
    {
        
    }
}
