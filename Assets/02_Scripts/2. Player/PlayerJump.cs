using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : Character
{
    [SerializeField]
    private float jumpSpeed;
    [SerializeField]
    private float _jumpPower;   //점프력

    bool isJump=false;

    EventParam eventParam = new EventParam();

    private Transform _transform;
    private bool _isJumping;
    private float _posY;        //오브젝트의 초기 높이
    private float _gravity;     //중력가속도
    private float _jumpTime;    //점프 이후 경과시간

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
        //y=-a*x+b에서 (a: 중력가속도, b: 초기 점프속도)
        //적분하여 y = (-a/2)*x*x + (b*x) 공식을 얻는다.(x: 점프시간, y: 오브젝트의 높이)
        //변화된 높이 height를 기존 높이 _posY에 더한다.
        float height = (_jumpTime * _jumpTime * (-_gravity) / 2) + (_jumpTime * _jumpPower);
        _transform.position = new Vector3(_transform.position.x, _posY + height, _transform.position.z);
        //점프시간을 증가시킨다.
        _jumpTime += Time.deltaTime*jumpSpeed;

        //처음의 높이 보다 더 내려 갔을때 => 점프전 상태로 복귀한다.
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
