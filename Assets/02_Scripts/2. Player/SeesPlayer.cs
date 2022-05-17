using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeesPlayer : FsmCondPolled
{
	public float fieldOfView;
	public float sightRange;

	public Transform Player;

	private float sightRangeSqr;

	void Awake()
	{
		sightRangeSqr = sightRange * sightRange;
	}

	// Use this for initialization
	void Start()
	{
		StartEvals();
	}

	private bool distanceOk(Vector3 toPlayer)
	{
		return toPlayer.sqrMagnitude <= sightRangeSqr;
	}

	private bool angleOk(Vector3 toPlayer)
	{
		float angle = Vector3.Angle(transform.forward, toPlayer);
		return angle <= fieldOfView / 2.0f;
	}

	private bool rayOk(Vector3 toPlayer)
	{
		bool oldSetting = Physics.queriesHitTriggers;
		Physics.queriesHitTriggers = false;

		RaycastHit hitInfo;
		bool ret = Physics.Raycast(transform.position, toPlayer, out hitInfo, toPlayer.magnitude) &&
			hitInfo.collider != null && hitInfo.collider.gameObject.GetComponent<PlayerMovement>() != null;

		Physics.queriesHitTriggers = oldSetting;

		return ret;
	}

	protected override bool EvaluateCondition()
	{
		Vector3 toPlayer = Player.position - transform.position;
		return distanceOk(toPlayer) && angleOk(toPlayer) && rayOk(toPlayer);
	}
}
