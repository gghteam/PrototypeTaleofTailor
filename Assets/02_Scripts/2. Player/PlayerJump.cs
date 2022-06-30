using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : Character
{
    [SerializeField]
    private float jumpPower = 10;

    EventParam eventParam = new EventParam();
    bool isFirst = false;
    private bool isJump = false;

    private readonly int hashJumpOn = Animator.StringToHash("JumpStart");
    private readonly int hashJumpOff = Animator.StringToHash("JumpStop");

    protected override void Awake()
    {
        base.Awake();
        EventManager.StartListening("InputJump", JumpOn);
    }

    protected override void Update()
    {
        ani.SetFloat("velocity", rigidbody.velocity.y);
        if (rigidbody.velocity.y <= 0)
        {
            isJump = false;
        }
        Debug.Log($"rigid velocity y : {rigidbody.velocity.y}");
    }

    private void OnCollisionEnter(Collision collision)
    {
        isJump = false;
        ani.SetBool(hashJumpOff, true);
    }

    void JumpOn(EventParam eventParam)
    {
        if (rigidbody.velocity.y <= 0)
            isJump = true;
        ani.SetTrigger(hashJumpOn);
        Jump();
    }

    void Jump()
    {
        if (isJump)
        {
            rigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }

    }

    private void OnDestroy()
    {
        EventManager.StopListening("InputJump", JumpOn);
    }
}
