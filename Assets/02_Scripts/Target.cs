using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    private void Start()
    {
        Vector3 l_vector = player.transform.position - transform.position;
        Quaternion qur = Quaternion.LookRotation(l_vector).normalized;
        qur = Quaternion.Euler(90, qur.eulerAngles.y, 0);
        transform.rotation = qur;
    }
}
