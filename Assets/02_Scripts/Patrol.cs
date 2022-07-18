using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : MonoBehaviour
{
	public Transform[] wayPoints;
	public int speed;
	public float distMin;
	public float waitSecound;

	private int wayPointsIndex;
	private bool isWaitDone = true;
	private NavMeshAgent nav;

	private void Start()
	{
		nav = GetComponent<NavMeshAgent>();
		wayPointsIndex = 0;
	}

	private void Update()
	{
		Patrols();
	}

	void Patrols()
	{
		nav.SetDestination(wayPoints[wayPointsIndex].position);
		if (nav.remainingDistance <= nav.stoppingDistance)
		{
			Vector3 v1 = wayPoints[wayPointsIndex].position;
			Gamemanager.Instance.Shuffle<Transform>(wayPoints);
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
