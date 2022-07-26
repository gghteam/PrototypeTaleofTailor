using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackCheck : MonoBehaviour
{
    [SerializeField]
    private PlayerAttack playerAttack;

    public bool isfirst = false;

    EventParam eventParam;

	private void Start()
	{
        EventManager.StartListening("Attack", Re);
	}

	private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("BOSS") && playerAttack.IsAttacking && !isfirst)
        {
            isfirst = true;
            eventParam.intParam = (int)playerAttack.PlayerDamage;
            Debug.Log($"BOSS HP: {(int)playerAttack.PlayerDamage}");
            eventParam.stringParam = "BOSS";
			EventManager.TriggerEvent("DAMAGE", eventParam); // µ¥¹ÌÁö
		}
        else if(other.CompareTag("ENEMY") && playerAttack.IsAttacking && !isfirst)
        {
            isfirst = true;
            other.GetComponent<EnemyHP>().Damage(1);

        }
    }

    private void Re(EventParam eventParam)
	{
        isfirst = false;
	}
}
