using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class EnemyDying : FsmState
{
	[SerializeField]
	private Animator animator;
	[SerializeField]
	private Renderer renderer;
	[SerializeField]
	private AudioSource sound;

	private float timer = 0;

    private void Start()
    {
		//animator = GetComponent<Animator>();
    }
    public override void OnStateEnter()
	{
		//GetComponent<Chase>().enabled = false;
		//GetComponent<Jump_Attack>().enabled = false;
		//GetComponent<EnemyAttack>().enabled = false;
		sound.Stop();
		NavMeshAgent agent = GetComponent<NavMeshAgent>();
		if (agent != null)
			agent.enabled = false;

		//foreach (Collider c in GetComponents<Collider>())
		//c.enabled = false;
		//transform.position = new Vector3(transform.position.x, 0, transform.position.y);
		animator.SetTrigger("IsDie");
	}

    private void Update()
    {
		//transform.localPosition = new Vector3(transform.localPosition.x, 0, transform.localPosition.z);
		if (timer >= 1)
		{
			renderer.material.SetFloat("_Dissolve", renderer.material.GetFloat("_Dissolve") + Time.deltaTime * 0.5f);

			if (renderer.material.GetFloat("_Dissolve") >= 0.9f)
			{
				Debug.Log("HIHI");
				//Destroy(gameObject);
			}
		}

		if(timer >= 5)
        {
			SceneManager.LoadScene("Clear");
        }
		timer += Time.deltaTime;
	}

}
