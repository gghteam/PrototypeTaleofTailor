using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCtrl : MonoBehaviour
{
    [SerializeField]
    private float speed = 5;
    private EventParam eventParam;

    private void Start()
    {
        eventParam.stringParam = "PLAYER";
        eventParam.intParam = 100;
    }
    private void Update()
    {
        transform.Translate((transform.forward * -1) * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("zzzz");
            EventManager.TriggerEvent("DAMAGE", eventParam);
            gameObject.SetActive(false);
        }
    }
}
