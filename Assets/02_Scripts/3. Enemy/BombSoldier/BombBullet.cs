using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBullet : MonoBehaviour
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

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        rb.AddForce(transform.forward* bombPower, ForceMode.Impulse);
        Invoke("BoomDamage", bombSec);
    }

    // ���� üũ
    void BoomDamage()
    {
        rb.angularVelocity = Vector3.zero;
        var hits = Physics.SphereCastAll(transform.position, radius, Vector3.up, 0f, LayerMask.GetMask("Controller"));
        foreach(var hit in hits)
        {
            // ��ź ���� ��
            if (hit.collider.CompareTag("Player"))
            {
                Debug.Log("��");
                eventParam.stringParam = "PLAYER";
                eventParam.intParam = damage;
                EventManager.TriggerEvent("DAMAGE", eventParam);
            }
        }
        EndBoom();
    }


    // ��ź�� ������
    void EndBoom()
    {
        Instantiate(bombParticle, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }
}
