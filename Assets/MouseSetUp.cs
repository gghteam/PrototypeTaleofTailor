using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseSetUp : MonoBehaviour
{
    private void OnEnable()
    {
        //커서 숨기기 및 위치 해제
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.None;
    }

    private void OnDisable()
    {
        //커서 숨기기 및 위치 고정
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            Cursor.visible = false;
        }
    }
}
