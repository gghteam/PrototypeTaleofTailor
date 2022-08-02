using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private float horizontal;
    private float vertical;
    // 움직임의 양
    private float moveAmount;

    private float mouseX;
    private float MouseY;

    //PlayerControls inputActions;

    Vector2 movementInput;
    Vector2 cameraInput;

    // X, Z의 움직임을 전달해주기 위한 구조체
    EventParam eventParam = new EventParam();
    EventParam cameraParam = new EventParam();

    private void Awake()
    {

        //커서 숨기기 및 위치 고정
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        MoveInput();
        AttackInput();
        JumpInput();
        LockInput();
        BandageItemInput();
        NeedleItemInput();
        ClothesButtonItemInput();
        TeaTableInput();
        TeleportInput();
    }

    /// <summary>
    /// 움직임에 대한 입력을 담당합니다.
    /// </summary>
    private void MoveInput()
    {
        movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        cameraInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        // X, Z 입력
        horizontal = movementInput.x;
        vertical = movementInput.y;

        // 움직임의 양 계산
        //Mathf.Clamp01 -> 강제로 0에서 1 범위로 변환
        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));

        // TO DO 추후 카메라에 사용
        mouseX = cameraInput.x;
        MouseY = cameraInput.y;

        //EventManager를 위한 Setting
        eventParam.vectorParam = new Vector2(horizontal, vertical);
        eventParam.intParam = (int)moveAmount;
        cameraParam.vectorParam = new Vector2(mouseX, MouseY);

        //움직임을 위한 신호 전송
        EventManager.TriggerEvent("INPUT", eventParam);
        EventManager.TriggerEvent("PLAYER_MOVEMENT", eventParam);
        EventManager.TriggerEvent("CAMERA_MOVE", cameraParam);
    }

    private void AttackInput()
    {
        eventParam.boolParam = Input.GetMouseButtonDown(0);

        EventManager.TriggerEvent("InputAttack", eventParam);
    }
    
    private void JumpInput()
    {
        eventParam.boolParam = Input.GetKeyDown(KeyCode.F);

        if (eventParam.boolParam)
            EventManager.TriggerEvent("InputJump", eventParam);
    }

    private void LockInput()
	{
        if (Input.GetMouseButtonDown(2))
            EventManager.TriggerEvent("LockOn", eventParam);
	}

    private void BandageItemInput()
    {
        eventParam.boolParam = Input.GetKeyDown(KeyCode.E);

        if (eventParam.boolParam)
            EventManager.TriggerEvent("BandageItem", eventParam);
    }
    private void NeedleItemInput()
    {
        eventParam.boolParam = Input.GetKeyDown(KeyCode.Q);

        if (eventParam.boolParam)
            EventManager.TriggerEvent("NeedleItem", eventParam);
    }
    
    private void ClothesButtonItemInput()
    {
        eventParam.boolParam = Input.GetKeyDown(KeyCode.R);

        if (eventParam.boolParam)
            EventManager.TriggerEvent("DanchuItem", eventParam);
    }
    
    private void TeaTableInput()
    {
        eventParam.boolParam = Input.GetKeyDown(KeyCode.T);

        if (eventParam.boolParam)
            EventManager.TriggerEvent("TeaTable", eventParam);
    }
    
    private void TeleportInput()
    {
        eventParam.boolParam = Input.GetKeyDown(KeyCode.Z);

        if (eventParam.boolParam)
            EventManager.TriggerEvent("Teleport", eventParam);
    }

    
}
