using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : Character
{
    [SerializeField]
    private float jumpPower = 10;

    bool isJump;

    EventParam eventParam = new EventParam();

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

    }

    private void OnCollisionEnter(Collision collision)
    {
        ani.SetBool(hashJumpOff, true);
        isJump = false;
        
    }

    void JumpOn(EventParam eventParam)
    {
        if (isJump) return;
        isJump = true;
        ani.SetTrigger(hashJumpOn);
        Jump();
    }

    void Jump()
    {
        rigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
    }

    private void OnDestroy()
    {
        EventManager.StopListening("InputJump", JumpOn);
    }
}
