using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossCutsceneStart : MonoBehaviour
{
    public UnityEvent DoorAction;
    public UnityEvent TriggerAction;

    EventParam eventParam = new EventParam();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EventManager.TriggerEvent("Rest", eventParam);
            StartCoroutine(Action());
        }
    }

    private IEnumerator Action()
    {
        DoorAction?.Invoke();
        yield return new WaitForSeconds(0.5f);
        TriggerAction?.Invoke();
    }
}
