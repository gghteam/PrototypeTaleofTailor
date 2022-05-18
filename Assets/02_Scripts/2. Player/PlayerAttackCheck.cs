using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackCheck : MonoBehaviour
{
    [SerializeField]
    private PlayerAttack playerAttack;

    public bool isfirst = false;

    EventParam eventParam;
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("BOSS") && playerAttack.IsAttacking && !isfirst)
        {
            isfirst = true;
            eventParam.intParam = (int)playerAttack.PlayerDamage;

            eventParam.intParam = 200; // ���ϴ� ������ ������ �ޱ�
            eventParam.stringParam = "PLAYER";
            EventManager.TriggerEvent("DAMAGE", eventParam); // ������ ��
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("BOSS"))
        {
            isfirst = false;
        }
    }
}
