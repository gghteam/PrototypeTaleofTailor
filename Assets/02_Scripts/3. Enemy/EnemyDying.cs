using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyDying : FsmState
{
	[SerializeField]
	private Animator animator;
	[SerializeField]
	private Renderer renderer;

	private float timer = 0;

    private void Start()
    {
		//animator = GetComponent<Animator>();
    }
    public override void OnStateEnter()
	{
		NavMeshAgent agent = GetComponent<NavMeshAgent>();
		if (agent != null)
			agent.enabled = false;

		//foreach (Collider c in GetComponents<Collider>())
			//c.enabled = false;

		animator.SetTrigger("IsDie");
	}

    private void Update()
    {
		if (timer >= 1)
		{
			renderer.material.SetFloat("_Dissolve", renderer.material.GetFloat("_Dissolve") + Time.deltaTime * 0.5f);

			if (renderer.material.GetFloat("_Dissolve") >= 0.9f)
			{
				Debug.Log("HIHI");
				Destroy(gameObject);
			}
		}
		timer += Time.deltaTime;
	}

}
