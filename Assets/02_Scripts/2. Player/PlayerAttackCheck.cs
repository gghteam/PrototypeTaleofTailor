using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackCheck : MonoBehaviour
{
    [SerializeField]
    private PlayerAttack playerAttack;

    EventParam eventParam;

	private void Start()
	{
        EventManager.StartListening("Attack", Re);
	}

	private void OnTriggerStay(Collider other)
    {
        if ((other.CompareTag("BOSS") && !playerAttack.isfirst) && playerAttack.IsAttacking)
        {
            playerAttack.isfirst = true;
            other.GetComponent<AudioSource>().Stop();
            other.GetComponent<AudioSource>().clip = VFXSet.Instance.playerAudioClip[(int)PlayerVFXs.Hit];
            other.GetComponent<AudioSource>().Play();
            eventParam.intParam = (int)playerAttack.PlayerDamage;
            Debug.Log($"BOSS HP: {(int)playerAttack.PlayerDamage}");
            eventParam.stringParam = "BOSS";
			EventManager.TriggerEvent("DAMAGE", eventParam); // µ¥¹ÌÁö
		}
        else if((other.CompareTag("ENEMY") && !playerAttack.isfirst) && playerAttack.IsAttacking)
        {
            playerAttack.isfirst = true;
            other.GetComponent<EnemyHP>().Damage(1);
            other.GetComponent<AudioSource>().Stop();
            other.GetComponent<AudioSource>().clip = VFXSet.Instance.playerAudioClip[(int)PlayerVFXs.Hit];
            other.GetComponent<AudioSource>().Play();
        }
    }

    private void Re(EventParam eventParam)
	{
        playerAttack.isfirst = false;
    }
}
