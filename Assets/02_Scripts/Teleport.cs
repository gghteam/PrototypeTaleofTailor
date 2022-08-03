using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    
    [SerializeField]
    GameObject panel;
    [SerializeField]
    Transform secondFloor;
    [SerializeField]
    float dis;

    bool isOn=false;

    private void Start()
    {
        EventManager.StartListening("Teleport", Teleporting);
    }
    void OnDestroy()
    {
        EventManager.StopListening("Teleport", Teleporting);

    }
    private void Update()
    {
        DisCheck();
    }
    void DisCheck()
    {
        //Debug.Log(Vector3.Distance(transform.position, player.transform.position));
        isOn = Vector3.Distance(transform.position, player.transform.position) < dis ? true : false;
        panel.SetActive(isOn);
    }

    void Teleporting(EventParam eventParam)
    {
        if(isOn) player.transform.position = secondFloor.position;
    }

}
