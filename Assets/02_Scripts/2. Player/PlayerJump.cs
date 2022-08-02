using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : Character
{
    [SerializeField]
    private float jumpSpeed;
    [SerializeField]
    private float _jumpPower;   //������

    bool isJump=false;

    EventParam eventParam = new EventParam();

    private Transform _transform;
    private bool _isJumping;
    private float _posY;        //������Ʈ�� �ʱ� ����
    private float _gravity;     //�߷°��ӵ�
    private float _jumpTime;    //���� ���� ����ð�

    void Start()
    {
        _transform = transform;
        _isJumping = false;
        _posY = transform.position.y;
        _gravity = 9.8f;
        _jumpTime = 0.0f;

        EventManager.StartListening("InputJump", JumpOn);
    }

    protected override void Update()
    {
        if (_isJumping)
        {
            Jump();
        }
    }
    void JumpOn(EventParam eventParam)
    {
        if (_isJumping) return;
        ani.SetTrigger("JumpStart");
        _isJumping = true;
        _posY = _transform.position.y;
    }
    void Jump()
    {
        //y=-a*x+b���� (a: �߷°��ӵ�, b: �ʱ� �����ӵ�)
        //�����Ͽ� y = (-a/2)*x*x + (b*x) ������ ��´�.(x: �����ð�, y: ������Ʈ�� ����)
        //��ȭ�� ���� height�� ���� ���� _posY�� ���Ѵ�.
        float height = (_jumpTime * _jumpTime * (-_gravity) / 2) + (_jumpTime * _jumpPower);
        _transform.position = new Vector3(_transform.position.x, _posY + height, _transform.position.z);
        //�����ð��� ������Ų��.
        _jumpTime += Time.deltaTime*jumpSpeed;

        //ó���� ���� ���� �� ���� ������ => ������ ���·� �����Ѵ�.
        if (height < 0.0f)
        {
            _isJumping = false;
            _jumpTime = 0.0f;
            _transform.position = new Vector3(_transform.position.x, _posY, _transform.position.z);
        }
    }
    private void OnDestroy()
    {
        EventManager.StopListening("InputJump", JumpOn);
    }
}
