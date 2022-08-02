using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : Character
{
    bool isGrounded;		// 땅에 서있는지 체크하기 위한 bool값
    public LayerMask ground;	// 레이어마스크 설정
    public float groundDistance = 0.2f;     // Ray를 쏴서 검사하는 거리
    public float jumpHeight = 3f;	// 점프 높이 설정

    void Start()
    {
        EventManager.StartListening("InputJump", JumpOn);
    }

    void JumpOn(EventParam eventParam)
    {
        //if (!isGrounded) return;
        ani.SetTrigger("JumpStart");
        Jump();
    }
    void FixedUpdate()
    {
        GroundCheck();	// 땅 위에 서있는지 체크
    }

    void Jump()
    {
        rigidbody.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
    }

    void GroundCheck()
    {
        RaycastHit hit;
        // 플레이어의 위치에서, 아래방향으로, groundDistance 만큼 ray를 쏴서, ground 레이어가 있는지 검사
        if (Physics.Raycast(transform.position, Vector3.down, out hit, groundDistance, ground))
        {
            Debug.Log("점프 가능");
            isGrounded = true;
        }
        else  isGrounded = false; 
    }

    private void OnDestroy()
    {
        EventManager.StopListening("InputJump", JumpOn);
    }
}
