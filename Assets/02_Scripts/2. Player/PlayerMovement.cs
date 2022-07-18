using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Character
{
	// EventParam���� ���� ����
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

	[SerializeField]
	private float turnSmoothing = 0.06f;

	private bool isDash = false;
	private float DashSpeed = 1;
	private bool isFirst = false;
	//private Vector3 dashDirection;
	private bool isMove = true;


	private void Start()
	{
		//Player �������� ���� ����
		EventManager.StartListening("PLAYER_MOVEMENT", SetMovement);
		EventManager.StartListening("ISDASH", IsDash);
		EventManager.StartListening("ISMOVE", IsMove);
		//��� ȣ�� �ϴ� ���� ����(����ȭ)
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
					//ĳ���� ��(inputZ = 1) �Ǵ� ��(inputZ = -1)�� vector�� ����
					moveDirection = cameraObject.forward * inputZ;
					//ĳ���� ������(inputZ = 1) �Ǵ� ����(inputZ = -1)�� vector�� ����
					moveDirection += cameraObject.right * inputX;
					moveDirection *= DashSpeed;
				}
				isFirst = false;
			}
		}
		else
		{
			//ĳ���� ��(inputZ = 1) �Ǵ� ��(inputZ = -1)�� vector�� ����
			moveDirection = cameraObject.forward * inputZ;
			//ĳ���� ������(inputZ = 1) �Ǵ� ����(inputZ = -1)�� vector�� ����
			moveDirection += cameraObject.right * inputX;
			//vector�� ����ȭ��(���̸� 1�� ����� ���⸸ ����)
		}

		moveDirection.y = 0;
		moveDirection.Normalize();

		if (ani.GetInteger("AttackCount") == 0)
		{
			if (moveDirection.sqrMagnitude > 0)
			{
				Rotating(inputX, inputZ);
				moveDirection.y = 0;
				if (Input.GetKey(KeyCode.LeftShift))
				{
					if (SteminaManager.Instance.CheckStemina(0.01f))
					{
						SteminaManager.Instance.MinusStemina(0.01f);
						//���⿡ Run_Speed�� ����
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
					//���⿡ Speed�� ����
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

		//���⿡ Speed�� ����
		//moveDirection *= movementSpeed;

		//normalVector�� ���� ������κ��� �÷��̾ �����̷��� ���⺤�ͷ� ����
		if (ani.GetInteger("AttackCount") == 0)
		{
			Vector3 projectedVelocity = Vector3.ProjectOnPlane(moveDirection, normalVector);
			//�̵�
			rigidbody.velocity = projectedVelocity;

			transform.LookAt(transform.position + moveDirection);
		}
	}

	Vector3 Rotating(float horizontal, float vertical)
	{
		Vector3 forward = cameraObject.TransformDirection(Vector3.forward);

		forward.y = 0.0f;
		forward = forward.normalized;

		Vector3 right = new Vector3(forward.z, 0, -forward.x);
		Vector3 targetDirection;
		targetDirection = forward * vertical + right * horizontal;

		// Lerp current direction to calculated target direction.
		if ((IsMoving() && targetDirection != Vector3.zero))
		{
			Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

			Quaternion newRotation = Quaternion.Slerp(rigidbody.rotation, targetRotation, turnSmoothing);
			rigidbody.MoveRotation(newRotation);
			SetLastDirection(targetDirection);
		}
		// If idle, Ignore current camera facing and consider last moving direction.
		if (!(Mathf.Abs(horizontal) > 0.9 || Mathf.Abs(vertical) > 0.9))
		{
			Repositioning();
		}

		return targetDirection;
	}

	public bool IsMoving()
	{
		return (inputX != 0) || (inputZ != 0);
	}

	private void SetLastDirection(Vector3 direction)
	{
		lastDirection = direction;
	}

	private void Repositioning()
	{
		if (lastDirection != Vector3.zero)
		{
			lastDirection.y = 0;
			Quaternion targetRotation = Quaternion.LookRotation(lastDirection);
			Quaternion newRotation = Quaternion.Slerp(rigidbody.rotation, targetRotation, turnSmoothing);
			rigidbody.MoveRotation(newRotation);
		}
	}

	/// <summary>
	/// �ı��Ǹ� �� �̻� �������� ���� �ʴ´�.
	/// </summary>
	private void OnDestroy()
	{
		EventManager.StopListening("PLAYER_MOVEMENT", SetMovement);
	}

	#region Movement
	Vector3 normalVector;

	/// <summary>
	/// Listening�� ���� Setting
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