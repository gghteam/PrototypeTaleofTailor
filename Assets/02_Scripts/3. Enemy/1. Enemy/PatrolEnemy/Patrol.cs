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


	public AnimationClip PatrolAnimClip = null;
	private Animation skullAnimation = null;
	private void Start()
	{
		skullAnimation = GetComponent<Animation>();
		nav = GetComponent<NavMeshAgent>();
		skullAnimation[PatrolAnimClip.name].wrapMode = WrapMode.Loop;
		wayPointsIndex = 0;
	}

	private void Update()
	{
		Patrols();
	}


	public override void OnStateEnter()
	{

	}
	public override void OnStateLeave()
	{

	}
	void Patrols()
	{
		skullAnimation.CrossFade(PatrolAnimClip.name);
		nav.SetDestination(wayPoints[wayPointsIndex].position);
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
