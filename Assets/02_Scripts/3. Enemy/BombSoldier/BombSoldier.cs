using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSoldier : MonoBehaviour
{
    // 1. Ÿ�� ��ġ�� �ִ� �� �ٶ󺸱�
    // - LookAt���� ȸ��

    // 2. �ٶ󺸴� �������� ������ �׸���
    // - ��ǥ���� �ޱ�(�÷��̾�)
    // - ��ǥ �������� ����(ang)��ŭ ���� �߻� AddForce

    // 3. ���𰡿� ��ų� �ٴڿ� ����� �� ��ź �Ͷ߸���

    [SerializeField, Header("����")]
    float ang;
    
    [SerializeField, Header("��ǥ ����")]
    Transform target;
    [SerializeField, Header("�Ѿ� ���� ��ġ")]
    Transform bombPos;
    
    [SerializeField, Header("��ź Prefab")]
    GameObject bomb;

    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        InvokeRepeating("BombAttack", 5f, 2f);
    }

    private void Update()
    {
        LookPlayer();
    }

    // �÷��̾� �Ĵٺ���
    void LookPlayer()
    {
        Vector3 vec = target.position - transform.position;
        vec.Normalize();
        Quaternion q = Quaternion.LookRotation(vec);
        transform.rotation = q;
    }

    // ��ź ����
    void BombAttack()
    {
        GameObject _bullet = Instantiate(bomb, bombPos.position, transform.rotation);
    }

}
