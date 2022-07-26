using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class CameraHandler : MonoBehaviour
{
	//[System.Obsolete]
	public Transform player;                                           // Player�� Transform
	public Vector3 pivotOffset = new Vector3(0.0f, 1.7f, 0.0f);        // ī�޶� ����Ű�� ���� Offset
	public Vector3 camOffset = new Vector3(0.0f, 0.0f, -3.0f);         // �÷��̾��� ��ġ�� ���õ� ī�޶� ���ġ�ϴ� Offset
	public float smooth = 10f;                                         // ī�޶� ������� �ӵ�
	public float horizontalAimingSpeed = 6f;                           // ���� ���� ȸ�� �ӵ�
	public float verticalAimingSpeed = 6f;                             // ���� ���� ȸ�� �ӵ�
	public float maxVerticalAngle = 30f;                               // ���� �ִ� ����
	public float minVerticalAngle = -60f;                              // ���� �ּ� ����

	public float yOffset = 3f;

	private float angleH = 0;                                          // ���콺 �̵��� ���� ���� ����
	private float angleV = 0;                                          // ���콺 �̵��� ���� ���� ����
	//[System.Obsolete]
	private Transform cam;                                             // �ش� ��ũ��Ʈ�� Transform
	private Vector3 smoothPivotOffset;                                 // ���� �� ���� ī�޶��� Pivot Offset�� ����
	private Vector3 smoothCamOffset;                                   // ���� �� ���� ī�޶��� Offset�� ����
	private Vector3 targetPivotOffset;                                 // Ÿ���� Pivot Offset
	private Vector3 targetCamOffset;                                   // ���� �� ī�޶��� Offset
	private float defaultFOV;                                          // �⺻ ī�޶��� �þ�
	private float targetFOV;                                           // Ÿ�� ī�޶��� �þ�
	private float targetMaxVerticalAngle;                              // ����� ���� �ִ� ���� ī�޶� ����
	private bool isCustomOffset;                                       // ����� ���� ī�޶� ������ ��� ����

	// ī�޶� ���� ���� ������Ƽ
	public float GetH { get { return angleH; } }

	[Header("���¿� �ʿ��� ������")]
	private bool lockOn = false;
	public float lockOnRange;
	public LayerMask layerMask;
	public CinemachineVirtualCamera camera = null;

	void Awake()
	{
		cam = transform;

		VCam.transform.position = Player.transform.position + Quaternion.identity * pivotOffset + Quaternion.identity * camOffset;
		VCam.transform.rotation = Quaternion.identity;

		smoothPivotOffset = pivotOffset;
		smoothCamOffset = camOffset;
		//defaultFOV = cam.GetComponent<Camera>().fieldOfView;
		defaultFOV = VCam.m_Lens.FieldOfView;
		angleH = Player.transform.eulerAngles.y;

		ResetTargetOffsets();
		ResetFOV();
		ResetMaxVerticalAngle();

		if (camOffset.y > 0)
			Debug.LogWarning("Vertical Cam Offset (Y) will be ignored during collisions!\n" +
				"It is recommended to set all vertical offset in Pivot Offset.");
	}

	public void Start()
	{
		EventManager.StartListening("LockOn", OnLockOn);
	}

	void Update()
	{
		angleH += Mathf.Clamp(Input.GetAxis("Mouse X"), -1, 1) * horizontalAimingSpeed;
		angleV += Mathf.Clamp(Input.GetAxis("Mouse Y"), -1, 1) * verticalAimingSpeed;

		angleV = Mathf.Clamp(angleV, minVerticalAngle, targetMaxVerticalAngle);

		Quaternion camYRotation = Quaternion.Euler(0, angleH, 0);
		Quaternion aimRotation = Quaternion.Euler(-angleV, angleH, 0);
		VCam.transform.rotation = aimRotation;

		VCam.m_Lens.FieldOfView = Mathf.Lerp(VCam.m_Lens.FieldOfView, targetFOV, Time.deltaTime);

		Vector3 baseTempPosition = Player.transform.position + camYRotation * targetPivotOffset;
		Vector3 noCollisionOffset = targetCamOffset;
		while (noCollisionOffset.magnitude >= 0.2f)
		{
			if (DoubleViewingPosCheck(baseTempPosition + aimRotation * noCollisionOffset))
				break;
			noCollisionOffset -= noCollisionOffset.normalized * 0.2f;
		}
		if (noCollisionOffset.magnitude < 0.2f)
			noCollisionOffset = Vector3.zero;

		bool customOffsetCollision = isCustomOffset && noCollisionOffset.sqrMagnitude < targetCamOffset.sqrMagnitude;

		smoothPivotOffset = Vector3.Lerp(smoothPivotOffset, customOffsetCollision ? pivotOffset : targetPivotOffset, smooth * Time.deltaTime);
		smoothCamOffset = Vector3.Lerp(smoothCamOffset, customOffsetCollision ? Vector3.zero : noCollisionOffset, smooth * Time.deltaTime);

		VCam.transform.position = Player.transform.position + camYRotation * smoothPivotOffset + aimRotation * smoothCamOffset;

		if (lockOn)
		{
			Collider[] colliders = Physics.OverlapSphere(this.transform.position, lockOnRange, layerMask);
			int maxindex = 0;
			Debug.Log(colliders.Length);
			for (int i = 0; i < colliders.Length; i++)
			{
				float maxDistance = (this.transform.position - colliders[maxindex].transform.position).sqrMagnitude;
				float enemyDistance = (this.transform.position - colliders[i].transform.position).sqrMagnitude;
				if (maxDistance < enemyDistance)
					maxindex = i;
			}
			player.LookAt(colliders[maxindex].transform);
			Vector3 vec = Player.transform.position + -player.forward.normalized * 10f;
			vec.y += 5;
			VCam.transform.position = vec;

			Vector3 playerVec = player.transform.position;
			playerVec.y += 2f;
			VCam.transform.LookAt(playerVec);
		}
	}

	public void SetTargetOffsets(Vector3 newPivotOffset, Vector3 newCamOffset)
	{
		targetPivotOffset = newPivotOffset;
		targetCamOffset = newCamOffset;
		isCustomOffset = true;
	}

	public void ResetTargetOffsets()
	{
		targetPivotOffset = pivotOffset;
		targetCamOffset = camOffset;
		isCustomOffset = false;
	}

	public void ResetYCamOffset()
	{
		targetCamOffset.y = camOffset.y;
	}

	public void SetYCamOffset(float y)
	{
		targetCamOffset.y = y;
	}

	public void SetXCamOffset(float x)
	{
		targetCamOffset.x = x;
	}

	public void SetFOV(float customFOV)
	{
		this.targetFOV = customFOV;
	}

	public void ResetFOV()
	{
		this.targetFOV = defaultFOV;
	}

	public void SetMaxVerticalAngle(float angle)
	{
		this.targetMaxVerticalAngle = angle;
	}

	public void ResetMaxVerticalAngle()
	{
		this.targetMaxVerticalAngle = maxVerticalAngle;
	}

	bool DoubleViewingPosCheck(Vector3 checkPos)
	{
		return ViewingPosCheck(checkPos) && ReverseViewingPosCheck(checkPos);
	}

	bool ViewingPosCheck(Vector3 checkPos)
	{
		Vector3 target = Player.transform.position + pivotOffset;
		Vector3 direction = target - checkPos;
		if (Physics.SphereCast(checkPos, 0.2f, direction, out RaycastHit hit, direction.magnitude))
		{
			if (hit.transform != Player && !hit.transform.GetComponent<Collider>().isTrigger)
			{
				return false;
			}
		}
		return true;
	}

	bool ReverseViewingPosCheck(Vector3 checkPos)
	{
		// Cast origin and direction.
		Vector3 origin = Player.transform.position + pivotOffset;
		Vector3 direction = checkPos - origin;
		if (Physics.SphereCast(origin, 0.2f, direction, out RaycastHit hit, direction.magnitude))
		{
			if (hit.transform != Player && hit.transform != transform && !hit.transform.GetComponent<Collider>().isTrigger)
			{
				return false;
			}
		}
		return true;
	}

	public float GetCurrentPivotMagnitude(Vector3 finalPivotOffset)
	{
		return Mathf.Abs((finalPivotOffset - smoothPivotOffset).magnitude);
	}

	public void OnLockOn(EventParam eventParam)
	{
		lockOn = !lockOn;
	}

	public void OnDestroy()
	{
		EventManager.StopListening("LockOn", OnLockOn);
	}
}
