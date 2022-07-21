using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSoldier : MonoBehaviour
{
    // 1. 타겟 위치가 있는 쪽 바라보기
    // - LookAt으로 회전

    // 2. 바라보는 방향으로 포물선 그리기
    // - 목표지점 받기(플레이어)
    // - 목표 지점에서 각도(ang)만큼 위로 발사 AddForce

    // 3. 무언가에 닿거나 바닥에 닿았을 떄 폭탄 터뜨리기

    [SerializeField, Header("각도")]
    float ang;
    
    [SerializeField, Header("목표 지점")]
    Transform target;
    [SerializeField, Header("총알 스폰 위치")]
    Transform bombPos;
    
    [SerializeField, Header("폭탄 Prefab")]
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

    // 플레이어 쳐다보기
    void LookPlayer()
    {
        Vector3 vec = target.position - transform.position;
        vec.Normalize();
        Quaternion q = Quaternion.LookRotation(vec);
        transform.rotation = q;
    }

    // 폭탄 생성
    void BombAttack()
    {
        GameObject _bullet = Instantiate(bomb, bombPos.position, transform.rotation);
    }

}
