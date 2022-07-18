using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 플레이어의 공통적인 부분을 추출
/// </summary>
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Character : MonoBehaviour
{
	protected PlayerState playerState = PlayerState.None;

	protected Rigidbody rigidbody = null;
	protected Collider col = null;
	protected Animator ani = null;
	protected Vector3 lastDirection;

	protected virtual void Awake()
	{
		rigidbody = GetComponent<Rigidbody>();
		col = GetComponent<Collider>();
		ani = GetComponent<Animator>();
	}

	protected virtual void Update()
    {

    }
}
