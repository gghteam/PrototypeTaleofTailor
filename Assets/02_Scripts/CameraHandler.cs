using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class CameraHandler : MonoBehaviour
{
	//[System.Obsolete]
	public Transform player;                                           // Player의 Transform
	public Vector3 pivotOffset = new Vector3(0.0f, 1.7f, 0.0f);        // 카메라를 가리키기 위한 Offset
	public Vector3 camOffset = new Vector3(0.0f, 0.0f, -3.0f);         // 플레이어의 위치와 관련된 카메라를 재배치하는 Offset
	public float smooth = 10f;                                         // 카메라 따라오는 속도
	public float horizontalAimingSpeed = 6f;                           // 수평 방향 회전 속도
	public float verticalAimingSpeed = 6f;                             // 수직 방향 회전 속도
	public float maxVerticalAngle = 30f;                               // 수직 최대 각도
	public float minVerticalAngle = -60f;                              // 수직 최소 각도

	private float angleH = 0;                                          // 마우스 이동을 통한 수평 각도
	private float angleV = 0;                                          // 마우스 이동을 통한 수직 각도
																	   //[System.Obsolete]
	private Transform cam;                                             // 해당 스크립트의 Transform
	private Vector3 smoothPivotOffset;                                 // 보간 시 현재 카메라의 Pivot Offset을 저장
	private Vector3 smoothCamOffset;                                   // 보간 시 현재 카메라의 Offset을 저장
	private Vector3 targetPivotOffset;                                 // 타켓의 Pivot Offset
	private Vector3 targetCamOffset;                                   // 보간 할 카메라의 Offset
	private float defaultFOV;                                          // 기본 카메라의 시야
	private float targetFOV;                                           // 타켓 카메라의 시야
	private float targetMaxVerticalAngle;                              // 사용자 지정 최대 수직 카메라 각도
	private bool isCustomOffset;                                       // 사용자 지정 카메라 오프셋 사용 여부

	// 카메라 수평 각도 프로퍼티
	public float GetH { get { return angleH; } }

	[Header("占쏙옙占승울옙 占십울옙占쏙옙 占쏙옙占쏙옙占쏙옙")]
	private bool lockOn = false;
	public float lockOnRange;
	public LayerMask layerMask;
	public Camera camera;
	private bool lockWait = false;

	void Awake()
	{
		cam = transform;

		//VCam.transform.position = Player.transform.position + Quaternion.identity * pivotOffset + Quaternion.identity * camOffset;
		//VCam.transform.rotation = Quaternion.identity;

		smoothPivotOffset = pivotOffset;
		smoothCamOffset = camOffset;
		//defaultFOV = cam.GetComponent<Camera>().fieldOfView;
		//defaultFOV = VCam.m_Lens.FieldOfView;
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
		if (lockOn)
		{
			LockOn();
			return;
		}
		else
			FLCam.gameObject.SetActive(true);
		//angleH += Mathf.Clamp(Input.GetAxis("Mouse X"), -1, 1) * horizontalAimingSpeed;
		//angleV += Mathf.Clamp(Input.GetAxis("Mouse Y"), -1, 1) * verticalAimingSpeed;

		//angleV = Mathf.Clamp(angleV, minVerticalAngle, targetMaxVerticalAngle);

		//Quaternion camYRotation = Quaternion.Euler(0, angleH, 0);
		//Quaternion aimRotation = Quaternion.Euler(-angleV, angleH, 0);
		////VCam.transform.rotation = aimRotation;

		////VCam.m_Lens.FieldOfView = Mathf.Lerp(VCam.m_Lens.FieldOfView, targetFOV, Time.deltaTime);

		//Vector3 baseTempPosition = Player.transform.position + camYRotation * targetPivotOffset;
		//Vector3 noCollisionOffset = targetCamOffset;
		//while (noCollisionOffset.magnitude >= 0.2f)
		//{
		//	if (DoubleViewingPosCheck(baseTempPosition + aimRotation * noCollisionOffset))
		//		break;
		//	noCollisionOffset -= noCollisionOffset.normalized * 0.2f;
		//}
		//if (noCollisionOffset.magnitude < 0.2f)
		//	noCollisionOffset = Vector3.zero;

		//bool customOffsetCollision = isCustomOffset && noCollisionOffset.sqrMagnitude < targetCamOffset.sqrMagnitude;

		//smoothPivotOffset = Vector3.Lerp(smoothPivotOffset, customOffsetCollision ? pivotOffset : targetPivotOffset, smooth * Time.deltaTime);
		//smoothCamOffset = Vector3.Lerp(smoothCamOffset, customOffsetCollision ? Vector3.zero : noCollisionOffset, smooth * Time.deltaTime);

		//VCam.transform.position = Player.transform.position + camYRotation * smoothPivotOffset + aimRotation * smoothCamOffset;

	}

	private void LockOn()
	{
		FLCam.gameObject.SetActive(false);
		Collider[] colliders = Physics.OverlapSphere(this.transform.position, lockOnRange, layerMask);
		int maxindex = 0;
		if (colliders.Length > 0)
		{
			for (int i = 1; i < colliders.Length; i++)
			{
				float maxDistance = Vector3.Distance(player.transform.position, colliders[maxindex].transform.position);
				float enemyDistance = Vector3.Distance(player.transform.position, colliders[i].transform.position);
				if (maxDistance > enemyDistance)
					maxindex = i;
			}
			player.LookAt(colliders[maxindex].transform);
			Vector3 vec = player.transform.position + -player.forward.normalized * 10f;
			vec.y += 5;
			camera.transform.position = vec;

			Vector3 playerVec = player.transform.position;
			playerVec.y += 2f;
			camera.transform.LookAt(playerVec);
		}
		else
			lockOn = false;
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
