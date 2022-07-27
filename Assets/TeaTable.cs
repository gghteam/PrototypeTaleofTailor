using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeaTable : MonoBehaviour
{
    [SerializeField]
    GameObject questions;
    [SerializeField]
    GameObject player;

    [SerializeField]
    float dis;

    bool isOn = false;

    private void Update()
    {
        CheckSave();
    }

    void CheckSave()
    {
        isOn = Vector3.Distance(transform.position, player.transform.position) < dis ? true:false;
        questions.SetActive(isOn);
    }

    private void Awake()
    {
        EventManager.StartListening("TeaTable", Use);
    }
    private void OnDestroy()
    {
        EventManager.StopListening("TeaTable", Use);
    }

    void Use(EventParam eventParam)
    {
        if (!isOn) return;

        EventManager.TriggerEvent("Rest", eventParam);
    }
}
