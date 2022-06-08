using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    EventParam eventParam = new EventParam();
    bool isFirst = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            eventParam.boolParam = true;
            EventManager.TriggerEvent("ISJUMP", eventParam);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        eventParam.boolParam = false;
        EventManager.TriggerEvent("ISJUMP", eventParam);
    }
}
