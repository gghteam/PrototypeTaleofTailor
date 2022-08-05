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
			EventManager.TriggerEvent("DAMAGE", eventParam); // ������
            eventParam.floatParam = 2f;
            eventParam.floatParam2 = 0.3f;
            EventManager.TriggerEvent("CameraShake", eventParam);
        }
        else if(other.CompareTag("ENEMY") && playerAttack.IsAttacking && !isfirst)
        {
            playerAttack.isfirst = true;
            other.GetComponent<EnemyHP>().Damage(1);
            ParticlePool hitParticle = PoolManager.Instance.Pop("CFX_Hit_C White") as ParticlePool;
            hitParticle.transform.position = other.transform.position + Vector3.up * 25;
            eventParam.floatParam = 2f;
            eventParam.floatParam2 = 0.3f;
            other.GetComponent<AudioSource>().Stop();
            other.GetComponent<AudioSource>().clip = VFXSet.Instance.playerAudioClip[(int)PlayerVFXs.Hit];
            other.GetComponent<AudioSource>().Play();
    }
    private void Re(EventParam eventParam)
        playerAttack.isfirst = false;
    }
