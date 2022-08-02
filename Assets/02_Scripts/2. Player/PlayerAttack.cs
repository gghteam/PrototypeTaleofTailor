using UnityEngine;

public class PlayerAttack : Character
{
	[SerializeField]
	private float playerDamage = 70;
	[SerializeField]
	private GameObject attackObject;
	[SerializeField]
	private ParticleSystem attackParticle;

	public float PlayerDamage { get { return playerDamage; } }

	private EventParam eventParam;

	private bool attacking = false;

	private int CountAttack;
	public bool IsAttacking { get { return attacking; } }

	private void Start()
	{
		EventManager.StartListening("InputAttack", IsInputAttack);
		EventManager.StartListening("PLUS_ATTACKPOWER", PlusAttackPower);
	}
	private void Update()
	{
		if (eventParam.boolParam)
		{
			Attack();
		}
	}
	private void PlayerAttackAnimationEnd()
	{
		CheckAttackPhase();
		EventManager.TriggerEvent("Attack", eventParam);
	}

	private void Attack()
	{
		if (CountAttack < 1)
		{
			attacking = true;
			ani.SetInteger("AttackCount", 1);
			CountAttack = 1;
		}
		if (ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.6f && ani.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
		{
			CountAttack++;
		}
	}

	private void CheckAttackPhase()
	{
		if (ani.GetCurrentAnimatorStateInfo(0).IsName("First Attack"))
		{
			if (CountAttack > 1)
			{
				ani.SetInteger("AttackCount", 2);
				CountAttack = 2;
			}
			else
			{
				ResetAttackPhase();
			}
		}
		else if (ani.GetCurrentAnimatorStateInfo(0).IsName("Secound Attack"))
		{
			if (CountAttack > 2)
			{
				ani.SetInteger("AttackCount", 3);
				CountAttack = 3;
			}
			else
			{
				ResetAttackPhase();
			}
		}
		else if (ani.GetCurrentAnimatorStateInfo(0).IsName("Third Attack"))
		{
			ResetAttackPhase();
		}
	}

	private void ResetAttackPhase()
	{
		CountAttack = 0;
		ani.SetInteger("AttackCount", 0);
		attacking = false;
	}

	private void IsInputAttack(EventParam ep)
	{
		eventParam.boolParam = ep.boolParam;
	}


	void PlusAttackPower(EventParam ep)
	{
		playerDamage += ep.intParam;
	}


	private void OnParticle(bool On)
	{
		attackParticle.gameObject.SetActive(On);
	}
}
