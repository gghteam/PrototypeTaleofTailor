using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerDash : Character
{
	[Header("�뽬 �ӵ�")]
	[SerializeField]
	private float smoothTime = 0.2f;

	[Header("�뽬 �Ÿ�")]
	[SerializeField]
	private float DashDistance;

	[Header("�뽬 ������Ʈ")]
	[SerializeField]
	private GameObject DashObjet;

	[Header("���̾� ���� �ϼ���")]
	[SerializeField]
	LayerMask layer;

	[Header("ī�޶� ������Ʈ")]
	[SerializeField]
	private Transform cameraObject;

	private Vector3 dashVec = Vector3.zero;

	private Vector3 input;
	private bool firstbool = false;
	private bool dashbool = false;
	private EventParam eventParam;


	private bool testBool = false;

	private void Start()
	{
		EventManager.StartListening("INPUT", getInput);
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space) && dashbool == false)
		{
			dashbool = true;
			firstbool = true;
			eventParam.boolParam = true;
			EventManager.TriggerEvent("ISDASH", eventParam);
			ani.SetBool("IsDash", dashbool);
		}

		if (dashbool)
		{
			Dash();
		}

		Debug.DrawLine(transform.position, dashVec);
	}
	RaycastHit ray;
	private void Dash()
	{
		if (firstbool)
		{

			Vector3 dir = (transform.localRotation * Vector3.forward).normalized;

			dashVec = new Vector3(dir.x * DashDistance + transform.position.x, transform.position.y, dir.z * DashDistance + transform.position.z);
			// Physics.BoxCast (�������� �߻��� ��ġ, �簢���� �� ��ǥ�� ���� ũ��, �߻� ����, �浹 ���, ȸ�� ����, �ִ� �Ÿ�)

			if (Physics.BoxCast(transform.position, new Vector3(DashDistance,DashDistance,DashDistance), dir, out ray, Quaternion.identity, layer))
			{
				testBool = true;
				Gizmos.color = Color.red;
				float vec = (transform.position-ray.point).magnitude;
				dashVec = new Vector3(dir.x * vec + transform.position.x, transform.position.y, dir.z * vec + transform.position.z);
				Debug.Log(dashVec);
			}


			firstbool = false;
		}

		//Vector3 smoothPosition = Vector3.SmoothDamp(
		//    transform.position,
		//    DashObjet.transform.position,
		//    ref lastMoveSpd,
		//    smoothTime
		//    );

		Vector3 smoothPosition = Vector3.Lerp(transform.position, dashVec, smoothTime * Time.deltaTime);

		Vector3 dirction = transform.position - dashVec;

		if (Mathf.RoundToInt(dirction.magnitude) > 0)
		{
			transform.position = smoothPosition;
		}
		else
		{
			dashbool = false;
			firstbool = true;
			eventParam.boolParam = false;
			EventManager.TriggerEvent("ISDASH", eventParam);
			ani.SetBool("IsDash", dashbool);
		}
	}

	private void getInput(EventParam eventParam)
	{
		input = eventParam.vectorParam;
	}
}
