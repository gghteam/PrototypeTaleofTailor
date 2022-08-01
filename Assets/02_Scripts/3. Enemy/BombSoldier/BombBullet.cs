using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBullet : PoolableMono
{
    [SerializeField, Header("입히는 데미지")]
    int damage = 50;
    [SerializeField, Header("터지는 시간")]
    float bombSec;
    [SerializeField, Header("속도")]
    float bombPower;

    [SerializeField, Header("범위")]
    float radius;
    [SerializeField, Header("폭발 파티클")]
    GameObject bombParticle;

    Rigidbody rb;
    EventParam eventParam;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        //rb.AddForce(transform.forward * bombPower, ForceMode.Impulse);
        Invoke("BoomDamage", bombSec);
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * bombPower * Time.deltaTime);
    }


    // 터지는 시간이 다달았을 때 말고도 플레이어나 다른 오브젝트에 닿았을 때도 체크해야한다.
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ENEMY") == false) // 부딫한개 내가 아니닐 때
        {
            BoomDamage();
        }
    }

    // 범위 체크
    void BoomDamage()
    {
        //rb.angularVelocity = Vector3.zero;
        rb.velocity = Vector3.zero;
        var hits = Physics.SphereCastAll(transform.position, radius, Vector3.up, 0f, LayerMask.GetMask("Controller"));
        foreach(var hit in hits)
        {
            // 폭탄 범위 안
            if (hit.collider.CompareTag("Player"))
            {
                eventParam.stringParam = "PLAYER";
                eventParam.intParam = damage;
                EventManager.TriggerEvent("DAMAGE", eventParam);
            }
        }
        EndBoom();
    }


    // 폭탄이 끝나고
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
