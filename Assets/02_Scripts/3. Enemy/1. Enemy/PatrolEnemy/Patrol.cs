using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : FsmState
{
	public Transform[] wayPoints;
	public int speed;
	public float distMin;
	public float waitSecound;

	private int wayPointsIndex;
	private NavMeshAgent nav;

	private FsmLegacyAni fsmLegacyAni;

	public bool isRandom = false;
	private void Awake()
	{
		fsmLegacyAni = GetComponent<FsmLegacyAni>();
	}
	private void Start()
	{
		nav = GetComponent<NavMeshAgent>();
		wayPointsIndex = 0;
	}

	private void Update()
	{
		if (isRandom)
			RandomPatrols();
		else
			Patrols();
	}


	public override void OnStateEnter()
	{
		fsmLegacyAni.ChangeAnimation(FsmLegacyAni.ClipState.Move, 0.25f);
	}
	public override void OnStateLeave()
	{

	}
	void RandomPatrols()
	{
		if (wayPoints.Length > 0)
		{
			nav?.SetDestination(wayPoints[wayPointsIndex].position);
			if (nav.remainingDistance <= nav.stoppingDistance)
			{
				Gamemanager.Instance.Shuffle<Transform>(wayPoints);
				Vector3 v1 = wayPoints[wayPointsIndex].position;
				if (v1 == wayPoints[wayPointsIndex].position)
				{
					wayPointsIndex++;
					if (wayPointsIndex >= wayPoints.Length)
					{
						wayPointsIndex = 0;
					}
				}
			}
		}
	}

	void Patrols()
	{
		if (wayPoints.Length > 0)
		{
			nav?.SetDestination(wayPoints[wayPointsIndex].position);
			if (nav.remainingDistance <= nav.stoppingDistance)
			{
				Vector3 v1 = wayPoints[wayPointsIndex].position;
				if (v1 == wayPoints[wayPointsIndex].position)
				{
					wayPointsIndex++;
					if (wayPointsIndex >= wayPoints.Length)
					{
						wayPointsIndex = 0;
					}
				}
			}
		}
	}

}
