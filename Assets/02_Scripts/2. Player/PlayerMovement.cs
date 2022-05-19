using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Character
{
	// EventParam한테 받은 값들
	private int inputX;
	private int inputZ;
	private int inputAmount;


	Transform cameraObject;
	Vector3 moveDirection;

	[HideInInspector]
	public Transform myTransform;


	[Header("Stats")]
	[SerializeField]
	private float movementSpeed = 5;
	[SerializeField]
	private float runMovementSpeed = 7;
	[SerializeField]
	private float rotationSpeed = 10;

	private bool isDash = false;
	private float DashSpeed = 1;
	private bool isFirst = false;
	//private Vector3 dashDirection;
	private bool isMove = true;

	private void Start()
	{
		//Player 움직임을 위해 들음
		EventManager.StartListening("PLAYER_MOVEMENT", SetMovement);
		EventManager.StartListening("ISDASH", IsDash);
		EventManager.StartListening("ISMOVE", IsMove);
		//계속 호출 하는 것을 방지(최적화)
		cameraObject = Camera.main.transform;
		myTransform = transform;
	}

	public void Update()
	{
		if (!isMove) return;
		if (isDash)
		{
			if (isFirst)
			{
				if (inputX == 0 && inputZ == 0)
				{
					moveDirection = cameraObject.forward;
					moveDirection *= DashSpeed;
				}
				else
				{
					//캐릭터 앞(inputZ = 1) 또는 뒤(inputZ = -1)를 vector에 저장
					moveDirection = cameraObject.forward * inputZ;
					//캐릭터 오른쪽(inputZ = 1) 또는 왼쪽(inputZ = -1)를 vector에 더함
					moveDirection += cameraObject.right * inputX;
					moveDirection *= DashSpeed;

				}
				isFirst = false;
			}
		}
		else
		{
			//캐릭터 앞(inputZ = 1) 또는 뒤(inputZ = -1)를 vector에 저장
			moveDirection = cameraObject.forward * inputZ;
			//캐릭터 오른쪽(inputZ = 1) 또는 왼쪽(inputZ = -1)를 vector에 더함
			moveDirection += cameraObject.right * inputX;
			//vector를 정규화함(길이를 1로 만들어 방향만 남김)
		}

		moveDirection.Normalize();
		if (!ani.GetBool("IsAttack"))
		{
			if (moveDirection.sqrMagnitude > 0)
			{
				transform.rotation = Quaternion.Euler(transform.rotation.x, cameraObject.eulerAngles.y, transform.rotation.z);
				moveDirection.y = 0;
				if (Input.GetKey(KeyCode.LeftShift))
				{
                    if (SteminaManager.Instance.CheckStemina(0.01f))
                    {
						SteminaManager.Instance.MinusStemina(0.01f);
						//방향에 Run_Speed를 곱함
						moveDirection *= runMovementSpeed;
						ani.SetBool("IsMove", false);
						ani.SetBool("IsRun", true);
					}
					
				}
				else if (isDash)
				{
					Debug.Log("?");
					moveDirection *= DashSpeed;
					ani.SetBool("IsMove", false);
					ani.SetBool("IsRun", false);
					ani.SetBool("IsDash", true);
				}
				else
				{
					//방향에 Speed를 곱함
					moveDirection *= movementSpeed;
					ani.SetBool("IsMove", true);
					ani.SetBool("IsRun", false);
				}
			}
			else
			{
				ani.SetBool("IsMove", false);
				ani.SetBool("IsRun", false);
			}
		}

		//방향에 Speed를 곱함
		//moveDirection *= movementSpeed;

		//normalVector의 법선 평면으로부터 플레이어가 움직이려는 방향벡터로 투영
		if (!ani.GetBool("IsAttack"))
		{
			Vector3 projectedVelocity = Vector3.ProjectOnPlane(moveDirection, normalVector);
			//이동
			rigidbody.velocity = projectedVelocity;
		}

		transform.LookAt(transform.position + moveDirection);
	}

	/// <summary>
	/// 파괴되면 더 이상 움직임을 듣지 않는다.
	/// </summary>
	private void OnDestroy()
	{
		EventManager.StopListening("PLAYER_MOVEMENT", SetMovement);
	}

	#region Movement
	Vector3 normalVector;

	/// <summary>
	/// Listening을 위한 Setting
	/// </summary>
	/// <param name="eventParam"></param>
	private void SetMovement(EventParam eventParam)
	{
		inputX = (int)eventParam.vectorParam.x;
		inputZ = (int)eventParam.vectorParam.y;
		inputAmount = eventParam.intParam;
	}
	private void HandleRotation(float delta)
	{
		Vector3 targetDir = Vector3.zero;
		float moveOverride = inputAmount;

		targetDir = cameraObject.forward * inputZ;
		targetDir += cameraObject.right * inputX;

		targetDir.Normalize();
		targetDir.y = 0;

		if (targetDir == Vector3.zero)
			targetDir = myTransform.forward;

		float rs = rotationSpeed;

		Quaternion tr = Quaternion.LookRotation(targetDir);
		Quaternion targetRotation = Quaternion.Slerp(myTransform.rotation, tr, rs * delta);

		myTransform.rotation = targetRotation;
	}
	#endregion

	private void IsDash(EventParam eventParam)
	{
		isDash = eventParam.boolParam;
		DashSpeed = eventParam.intParam;
		isFirst = eventParam.boolParam2;
	}
	private void IsMove(EventParam eventParam)
	{
		isMove = eventParam.boolParam;
	}
}