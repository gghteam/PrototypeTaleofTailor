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
    private Collider hitCollider;

    private EventParam eventParam;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !eventParam.boolParam)
        {
            eventParam.intParam = (int)smoothTime;
            eventParam.boolParam = true;
            eventParam.boolParam2 = true;
            EventManager.TriggerEvent("ISDASH", eventParam);
            StartCoroutine(StartDash());
        }
    }


    private IEnumerator StartDash()
    {
        hitCollider.enabled = false;
        yield return new WaitForSeconds(dashtime);
        hitCollider.enabled = true;
        eventParam.boolParam = false;
        eventParam.boolParam2 = false;
        EventManager.TriggerEvent("ISDASH", eventParam);
    }
}