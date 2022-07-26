using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCtrl : MonoBehaviour
{
    private void Update()
    {
        transform.Translate((transform.forward * -1) * 5 * Time.deltaTime);
    }
}
