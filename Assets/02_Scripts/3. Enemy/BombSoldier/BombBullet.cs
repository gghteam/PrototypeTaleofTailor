using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBullet : MonoBehaviour
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
        rb.AddForce(transform.forward* bombPower, ForceMode.Impulse);
        Invoke("BoomDamage", bombSec);
    }

    // 범위 체크
    void BoomDamage()
    {
        rb.angularVelocity = Vector3.zero;
        var hits = Physics.SphereCastAll(transform.position, radius, Vector3.up, 0f, LayerMask.GetMask("Controller"));
        foreach(var hit in hits)
        {
            // 폭탄 범위 안
            if (hit.collider.CompareTag("Player"))
            {
                Debug.Log("펑");
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
        Instantiate(bombParticle, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }
}
