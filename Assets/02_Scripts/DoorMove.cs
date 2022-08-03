using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DoorMove : MonoBehaviour
{
    [SerializeField]
    private Vector3 closeRot;
    [SerializeField]
    private Vector3 openRot;

    public void Open(bool isOpne)
    {
        if (isOpne == true)
        {
            transform.DORotate(openRot, 0.5f, RotateMode.Fast);
        }
        else
        {
            transform.DORotate(closeRot, 0.5f, RotateMode.Fast);
        }
    }
}
