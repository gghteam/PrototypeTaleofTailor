using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCtrl : MonoBehaviour
{
    [SerializeField]
    private float speed = 5;
    private void Update()
    {
        transform.Translate((transform.forward * -1) * speed * Time.deltaTime);
    }
}
