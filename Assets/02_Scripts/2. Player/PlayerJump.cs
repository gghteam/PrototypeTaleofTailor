using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : Character
{
    bool isGrounded;		// ���� ���ִ��� üũ�ϱ� ���� bool��
    public LayerMask ground;	// ���̾��ũ ����
    public float groundDistance = 0.2f;     // Ray�� ���� �˻��ϴ� �Ÿ�
    public float jumpHeight = 3f;	// ���� ���� ����

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
        GroundCheck();	// �� ���� ���ִ��� üũ
    }

    void Jump()
    {
        rigidbody.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
    }

    void GroundCheck()
    {
        RaycastHit hit;
        // �÷��̾��� ��ġ����, �Ʒ���������, groundDistance ��ŭ ray�� ����, ground ���̾ �ִ��� �˻�
        if (Physics.Raycast(transform.position, Vector3.down, out hit, groundDistance, ground))
        {
            Debug.Log("���� ����");
            isGrounded = true;
        }
        else  isGrounded = false; 
    }

    private void OnDestroy()
    {
        EventManager.StopListening("InputJump", JumpOn);
    }
}
