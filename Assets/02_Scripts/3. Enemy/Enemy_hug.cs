using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_hug : MonoBehaviour
{
	[SerializeField]
	private GameObject playerObject;

	[SerializeField]
	private Transform leftHand;
	[SerializeField]
	private Transform rightHand;

	private Animator ani;
	//잡는 시기 변수
	private bool huging = false;
	//잡혔는지 안잡혔는지 변수
	private bool isfirst = true;
	//어 그정도의 바운더안에 들어오면 


	private EventParam eventParam;
	private void Start()
	{
		ani = GetComponent<Animator>();
	}

	private void Update()
	{
		if (a() < 1.5f && !ani.GetBool("IsHug"))
		{
			//애니메이션을 플레이 시켜준다.
			ani.SetBool("IsHug", true);
			isfirst = true;
		}
		Hug();
	}


	private void Hug()
	{
		if (ani.GetBool("IsHug") && (ani.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.4f && ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.3f))
		{
			huging = true;
		}
		else if (ani.GetBool("IsHug") && ani.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.4f && isfirst)
		{
			ani.SetBool("IsHug", false);
		}
		else if (ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
		{
			isfirst = true;
			playerObject.transform.position = new Vector3(playerObject.transform.position.x, 1, playerObject.transform.position.z);
			//데미지 달게하고
		}
		else if (!isfirst)
		{
			playerObject.transform.position = leftHand.transform.position;
		}
		else if(ani.GetCurrentAnimatorStateInfo(0).IsName("Idle") && huging)
		{
			Debug.Log("?");
			eventParam.intParam = 200; // 원하는 정도의 데미지 받기
			eventParam.stringParam = "PLAYER";
			EventManager.TriggerEvent("DAMAGE", eventParam); // 데미지 입었다는 이벤트 신호 보내기
			huging = false;
		}
	}

	private void OnTriggerStay(Collider other)
	{
		if (huging && isfirst && other.CompareTag("Player"))
		{
			isfirst = false;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if(other.CompareTag("Player"))
		{

		}
	}


	private float a()
	{
		return (playerObject.transform.position - transform.position).magnitude;
	}
}