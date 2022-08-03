using UnityEngine;

public class PlayerClothesButton : ItemManager
{
    [SerializeField, Header("���� ������ �� �÷��̾���� �Ÿ�")]
    float buttonDistance = 5f;

    [SerializeField, Header("����")]
    GameObject[] clothesButton;

    int danchuIndex = 3;    // ���� ��� ����
    int clothesButtonItemCount = 0;    // �ֿ� ���� ����

    EventParam eventParam = new EventParam();

    private void Start()
    {
        // �׾��� �� �̺�Ʈ �ޱ� ( ���� ������ )
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


    // ������ ���
    protected override void UseItem(EventParam eventParam)
    {
        if (clothesButtonItemCount > 0)
        {
            isUsing = true;
            clothesButtonItemCount--;
            ClothesButtonCount();
        EventManager.TriggerEvent("PLUSCLOTHESBUTTON", eventParam); // ���� ȸ��
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
        EventManager.TriggerEvent("ITEMTEXT", eventParam); //���� ǥ��
    }

    // ���� ������ â ���� Ű��
    void ClothesButtonOnOff(bool isOpen)
    {
        eventParam.itemParam = Item.CLOTHES_BUTTON;
        eventParam.boolParam = isOpen;
        EventManager.TriggerEvent("ITEMHAVE", eventParam);
    }

    //���� ����߸��� ���� (���� ����Ʈ�� ���� �Լ� �߰�)
    void DropClothesButton(EventParam eventParam)
    {
        //���� �����ְ� ����
        danchuIndex = eventParam.intParam;
        SetClothesTransform();
    }

    //������ ���� ���ϰ� ����
    void SetClothesTransform()
    {
        //���� ����
        if (danchuIndex % 2 == 0) // ���߰� ���ɰ����� ����
        {
            danchuIndex = danchuIndex / 2 - 1;
        }
        else // ���߰� ���ɰ����� �ִ�
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
        ClothesButtonOnOff(true); // ���� ������ â �̹��� Ű��
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
