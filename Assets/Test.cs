using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public LayerMask layer;
    public float distance;
    public Vector3 cubeScale;

    // Update is called once per frame
    void Update()
    {
        MyCollisions();
    }

    void MyCollisions()
    {

        Collider[] hitColliders = Physics.OverlapBox(transform.position + transform.forward * distance, cubeScale / 2, Quaternion.identity, layer);

        int i = 0;
        while (i < hitColliders.Length)
        {
            Debug.Log("Hit : " + hitColliders[i].name);
            i++;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + transform.forward * distance, cubeScale);
    }
}
