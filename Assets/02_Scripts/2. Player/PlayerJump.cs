using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField]
    private float jumpPower = 10;

    Rigidbody rb;

    EventParam eventParam = new EventParam();
    bool isFirst = false;
    private bool isJump = false;


    private void Awake()
    {
        EventManager.StartListening("InputJump", Jump);
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
         isJump = false;
    }

    void Jump(EventParam eventParam)
    {
        if (isJump) return;
        isJump = true;
        rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
    }

    private void OnDestroy()
    {
        EventManager.StopListening("InputJump", Jump);
    }
}
