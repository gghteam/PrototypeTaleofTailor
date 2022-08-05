using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash2 : Character
{
	[Header("대쉬 스피드")]
	[SerializeField]
	private float smoothTime = 0.2f;

	[Header("대쉬 시간")]
	[SerializeField]
	private float dashtime = 0f;

	[SerializeField]
	private GameObject dashParticle;

	[SerializeField]
	private Collider hitCollider;

	private EventParam eventParam;

	protected override void Awake()
	{
		base.Awake();
		dashParticle.gameObject.SetActive(false);
	}
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space) && !eventParam.boolParam && ani.GetInteger("AttackCount") == 0)
		{
			if (SteminaManager.Instance.CheckStemina(1))
			{
				SteminaManager.Instance.MinusStemina(1);
				eventParam.intParam = (int)smoothTime;
				eventParam.boolParam = true;
				eventParam.boolParam2 = true;
				EventManager.TriggerEvent("ISDASH", eventParam);
				StartCoroutine(StartDash());
			}

		}
	}


	private IEnumerator StartDash()
	{
		ani.SetBool("IsDash", true);
		hitCollider.enabled = false;
		dashParticle.gameObject.SetActive(true);
		yield return new WaitForSeconds(dashtime);
		hitCollider.enabled = true;
		dashParticle.gameObject.SetActive(false);
		ani.SetBool("IsDash", false);
		eventParam.boolParam = false;
		eventParam.boolParam2 = false;
		EventManager.TriggerEvent("ISDASH", eventParam);
	}
}