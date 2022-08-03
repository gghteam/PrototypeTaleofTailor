using UnityEngine;

public class PlayerClothesButton : ItemManager
{
    [SerializeField, Header("단추 떨어질 때 플레이어와의 거리")]
    float buttonDistance = 5f;

    [SerializeField, Header("단추")]
    GameObject[] clothesButton;

    int danchuIndex = 3;    // 현재 목숨 단추
    int clothesButtonItemCount = 0;    // 주운 단추 갯수

    EventParam eventParam = new EventParam();

    private void Start()
    {
        // 죽었을 때 이벤트 받기 ( 단추 떨구기 )
        clothesButtonItemCount = 0;
        EventManager.StartListening("DEAD", DropClothesButton);
        EventManager.StartListening("DanchuItem", UseItem);
        EventManager.StartListening("ResetDanchu", ResetClothes);
    }
    private void OnDestroy()
    {
        EventManager.StopListening("DEAD", DropClothesButton);
        EventManager.StopListening("DanchuItem", UseItem);
        EventManager.StopListening("ResetDanchu", ResetClothes);
    }


    // 아이템 사용
    protected override void UseItem(EventParam eventParam)
    {
        if (clothesButtonItemCount > 0)
        {
            isUsing = true;
            clothesButtonItemCount--;
            ClothesButtonCount();
        EventManager.TriggerEvent("PLUSCLOTHESBUTTON", eventParam); // 단추 회복
        ClothesUseAnim();
        }
        if (clothesButtonItemCount <= 0)
        {
            ItemZero();
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
        if (danchuIndex % 2 == 0) // 단추가 반쪼가리가 없다
        {
            danchuIndex = danchuIndex / 2 - 1;
        }
        else // 단추가 반쪼가리가 있다
        {
            danchuIndex = (danchuIndex - 1) / 2 - 1;
        }
        SpawnClothesButton();
    }

    void SpawnClothesButton()
    {
        int spawnX=0, spawnZ=0;
        int randomPos = Random.Range(1, 100);
        if (randomPos <= 30)
        {
            spawnX = 20;
            spawnZ = 20;
        }
        else if (randomPos <= 50)
        {
            spawnX = -20;
            spawnZ = -20;
        }
        else if (randomPos <= 80)
        {
            spawnX = 20;
            spawnZ = -20;
        }
        else
        {
            spawnX = -20;
            spawnZ = 20;
        }
        Vector3 pos = transform.position + new Vector3(spawnX, 0, spawnZ);
        pos.y = transform.position.y+2.5f;
        clothesButton[danchuIndex].transform.localPosition = pos;
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
        eventParam.boolParam = false;
        EventManager.TriggerEvent("ITEMOFF", eventParam);
        baseWeapon.SetActive(true);
        item.SetActive(false);
        isUsing = false;
    }

    void ResetClothes(EventParam eventParam)
    {
        ItemZero();
        int index = eventParam.intParam/2;
        danchuIndex = index;
        clothesButtonItemCount = 0;
        for(int i=0;i< index; i++)
        {
            clothesButton[i].SetActive(false);
        }
    }
}
