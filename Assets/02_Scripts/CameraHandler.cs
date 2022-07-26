using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class CameraHandler : MonoBehaviour
{
	//[System.Obsolete]
<<<<<<< HEAD
	public Transform player;                                           // PlayerÀÇ Transform
	public Vector3 pivotOffset = new Vector3(0.0f, 1.7f, 0.0f);        // Ä«¸Þ¶ó¸¦ °¡¸®Å°±â À§ÇÑ Offset
	public Vector3 camOffset = new Vector3(0.0f, 0.0f, -3.0f);         // ÇÃ·¹ÀÌ¾îÀÇ À§Ä¡¿Í °ü·ÃµÈ Ä«¸Þ¶ó¸¦ Àç¹èÄ¡ÇÏ´Â Offset
	public float smooth = 10f;                                         // Ä«¸Þ¶ó µû¶ó¿À´Â ¼Óµµ
	public float horizontalAimingSpeed = 6f;                           // ¼öÆò ¹æÇâ È¸Àü ¼Óµµ
	public float verticalAimingSpeed = 6f;                             // ¼öÁ÷ ¹æÇâ È¸Àü ¼Óµµ
	public float maxVerticalAngle = 30f;                               // ¼öÁ÷ ÃÖ´ë °¢µµ
	public float minVerticalAngle = -60f;                              // ¼öÁ÷ ÃÖ¼Ò °¢µµ

	private float angleH = 0;                                          // ¸¶¿ì½º ÀÌµ¿À» ÅëÇÑ ¼öÆò °¢µµ
	private float angleV = 0;                                          // ¸¶¿ì½º ÀÌµ¿À» ÅëÇÑ ¼öÁ÷ °¢µµ
																	   //[System.Obsolete]
	private Transform cam;                                             // ÇØ´ç ½ºÅ©¸³Æ®ÀÇ Transform
	private Vector3 smoothPivotOffset;                                 // º¸°£ ½Ã ÇöÀç Ä«¸Þ¶óÀÇ Pivot OffsetÀ» ÀúÀå
	private Vector3 smoothCamOffset;                                   // º¸°£ ½Ã ÇöÀç Ä«¸Þ¶óÀÇ OffsetÀ» ÀúÀå
	private Vector3 targetPivotOffset;                                 // Å¸ÄÏÀÇ Pivot Offset
	private Vector3 targetCamOffset;                                   // º¸°£ ÇÒ Ä«¸Þ¶óÀÇ Offset
	private float defaultFOV;                                          // ±âº» Ä«¸Þ¶óÀÇ ½Ã¾ß
	private float targetFOV;                                           // Å¸ÄÏ Ä«¸Þ¶óÀÇ ½Ã¾ß
	private float targetMaxVerticalAngle;                              // »ç¿ëÀÚ ÁöÁ¤ ÃÖ´ë ¼öÁ÷ Ä«¸Þ¶ó °¢µµ
	private bool isCustomOffset;                                       // »ç¿ëÀÚ ÁöÁ¤ Ä«¸Þ¶ó ¿ÀÇÁ¼Â »ç¿ë ¿©ºÎ

	// Ä«¸Þ¶ó ¼öÆò °¢µµ ÇÁ·ÎÆÛÆ¼
=======
	public Transform player;                                           // Playerï¿½ï¿½ Transform
	public Vector3 pivotOffset = new Vector3(0.0f, 1.7f, 0.0f);        // Ä«ï¿½Þ¶ï¿½ ï¿½ï¿½ï¿½ï¿½Å°ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ Offset
	public Vector3 camOffset = new Vector3(0.0f, 0.0f, -3.0f);         // ï¿½Ã·ï¿½ï¿½Ì¾ï¿½ï¿½ï¿½ ï¿½ï¿½Ä¡ï¿½ï¿½ ï¿½ï¿½ï¿½Ãµï¿½ Ä«ï¿½Þ¶ï¿½ ï¿½ï¿½ï¿½Ä¡ï¿½Ï´ï¿½ Offset
	public float smooth = 10f;                                         // Ä«ï¿½Þ¶ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½Óµï¿½
	public float horizontalAimingSpeed = 6f;                           // ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ È¸ï¿½ï¿½ ï¿½Óµï¿½
	public float verticalAimingSpeed = 6f;                             // ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ È¸ï¿½ï¿½ ï¿½Óµï¿½
	public float maxVerticalAngle = 30f;                               // ï¿½ï¿½ï¿½ï¿½ ï¿½Ö´ï¿½ ï¿½ï¿½ï¿½ï¿½
	public float minVerticalAngle = -60f;                              // ï¿½ï¿½ï¿½ï¿½ ï¿½Ö¼ï¿½ ï¿½ï¿½ï¿½ï¿½

	public float yOffset = 3f;

	private float angleH = 0;                                          // ï¿½ï¿½ï¿½ì½º ï¿½Ìµï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½
	private float angleV = 0;                                          // ï¿½ï¿½ï¿½ì½º ï¿½Ìµï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½
	//[System.Obsolete]
	private Transform cam;                                             // ï¿½Ø´ï¿½ ï¿½ï¿½Å©ï¿½ï¿½Æ®ï¿½ï¿½ Transform
	private Vector3 smoothPivotOffset;                                 // ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ Ä«ï¿½Þ¶ï¿½ï¿½ï¿½ Pivot Offsetï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½
	private Vector3 smoothCamOffset;                                   // ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ Ä«ï¿½Þ¶ï¿½ï¿½ï¿½ Offsetï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½
	private Vector3 targetPivotOffset;                                 // Å¸ï¿½ï¿½ï¿½ï¿½ Pivot Offset
	private Vector3 targetCamOffset;                                   // ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ Ä«ï¿½Þ¶ï¿½ï¿½ï¿½ Offset
	private float defaultFOV;                                          // ï¿½âº» Ä«ï¿½Þ¶ï¿½ï¿½ï¿½ ï¿½Ã¾ï¿½
	private float targetFOV;                                           // Å¸ï¿½ï¿½ Ä«ï¿½Þ¶ï¿½ï¿½ï¿½ ï¿½Ã¾ï¿½
	private float targetMaxVerticalAngle;                              // ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ ï¿½Ö´ï¿½ ï¿½ï¿½ï¿½ï¿½ Ä«ï¿½Þ¶ï¿½ ï¿½ï¿½ï¿½ï¿½
	private bool isCustomOffset;                                       // ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ Ä«ï¿½Þ¶ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½

	// Ä«ï¿½Þ¶ï¿½ ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½Æ¼
>>>>>>> origin/NEWKJP
	public float GetH { get { return angleH; } }

	[Header("ï¿½ï¿½ï¿½Â¿ï¿½ ï¿½Ê¿ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½")]
	private bool lockOn = false;
	public float lockOnRange;
	public LayerMask layerMask;
	public CinemachineVirtualCamera camera = null;
	private bool lockWait = false;

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
		if (lockOn)
		{
			LockOn();
			return;
		}
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

	}

	private void LockOn()
	{
		Collider[] colliders = Physics.OverlapSphere(this.transform.position, lockOnRange, layerMask);
		int maxindex = 0;
		for (int i = 1; i < colliders.Length; i++)
		{
			float maxDistance = Vector3.Distance(player.transform.position, colliders[maxindex].transform.position);
			float enemyDistance = Vector3.Distance(player.transform.position,colliders[i].transform.position);
			if (maxDistance > enemyDistance)
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
