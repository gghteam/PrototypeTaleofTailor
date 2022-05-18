using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClothesButton : ItemManager
{
    [SerializeField, Header("단추 떨어질 때 플레이어와의 거리")]
    float buttonDistance = 5f;

    [SerializeField, Header("단추")]
    GameObject[] clothesButton;

    int danchuIndex = 3;    // 현재 목숨 단추
    int clothesButtonItemCount = 0;    // 주운 단추 갯수

    Vector3 enemyVector = Vector3.zero;    // Enemy의 위치

    EventParam eventParam = new EventParam();

    private void Start()
    {
        // 죽었을 때 이벤트 받기 ( 단추 떨구기 )
        EventManager.StartListening("DEAD", DropClothesButton);
    }
    private void OnDestroy()
    {
        EventManager.StopListening("DEAD", DropClothesButton);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))   // 단추 아이템 사용
        {
            UseItem();
        }
    }

    // 아이템 사용
    protected override void UseItem()
    {
        if (isUsing) return;
        if (clothesButtonItemCount > 0) // 갯수가 0이 아니면 사용
        {
            isUsing = true;
            clothesButtonItemCount--;
            ClothesButtonCount();
            eventParam.itemParam = Item.CLOTHES_BUTTON;
            ClothesUseAnim();
            // 아이템이 이제 없다면 이미지 끄기
            if (clothesButtonItemCount >= 0)
            {
                ItemZero();
                EventManager.TriggerEvent("PLUSCLOTHESBUTTON", eventParam); // 단추 회복
            }
        }
    }
    void ClothesUseAnim()
    {
        item.SetActive(true);
        baseWeapon.SetActive(false);
        eventParam.itemParam = Item.CLOTHES_BUTTON;
        EventManager.TriggerEvent("ITEMUSEANIM", eventParam);
        Invoke("ClothesButtonStop", useTime);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("BOSS"))
        {
            enemyVector = collision.transform.position; // 적의 위치 받기
        }
        if (collision.collider.CompareTag("CLOTHESBUTTON")) // 떨어진 단추 주웠을 때
        {
            collision.gameObject.SetActive(false);  // 주운 단추 안보이게 하기
            GetItem();
        }
    }
    void ClothesButtonCount()
    {
        eventParam.intParam = clothesButtonItemCount;
        eventParam.itemParam = Item.CLOTHES_BUTTON;
        EventManager.TriggerEvent("ITEMTEXT", eventParam); //갯수 표시
    }

    // 단추 아이템 창 끄고 키기
    void ClothesButtonOnOff(bool isOpen)
    {
        eventParam.itemParam = Item.CLOTHES_BUTTON;
        eventParam.boolParam = isOpen;
        EventManager.TriggerEvent("ITEMHAVE", eventParam);
    }

    //단추 떨어뜨리기 실행 (나중 이펙트를 위해 함수 추가)
    void DropClothesButton(EventParam eventParam)
    {
        //방향 구해주고 세팅
        danchuIndex = eventParam.intParam;
        SetClothesTransform();
    }

    //떨어질 방향 구하고 세팅
    void SetClothesTransform()
    {
        //단추 생성
        Vector3 buttonPos = (transform.position - enemyVector).normalized;
        if (danchuIndex % 2 == 0) // 단추가 반쪼가리가 없다
        {
            danchuIndex = danchuIndex / 2 - 1;
        }
        else // 단추가 반쪼가리가 있다
        {
            danchuIndex = (danchuIndex - 1) / 2 - 1;
        }
        clothesButton[danchuIndex].transform.localPosition = new Vector3(buttonPos.x * buttonDistance, 0.5f, buttonPos.z * buttonDistance);
        clothesButton[danchuIndex].gameObject.SetActive(true);
    }

    protected override void GetItem()
    {
        clothesButtonItemCount++;
        ClothesButtonCount();
        ClothesButtonOnOff(true); // 단추 아이템 창 이미지 키기
    }

    protected override void ItemZero()
    {
        ClothesButtonOnOff(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CLOTHESBUTTON"))
        {
            other.gameObject.SetActive(false);
            GetItem();
        }
    }

    void ClothesButtonStop()
    {
        baseWeapon.SetActive(true);
        item.SetActive(false);
        isUsing = false;
    }
}
