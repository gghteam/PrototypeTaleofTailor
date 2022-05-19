using UnityEngine;

public class PlayerClothesButton : ItemManager
{
    [SerializeField, Header("���� ������ �� �÷��̾���� �Ÿ�")]
    float buttonDistance = 5f;

    [SerializeField, Header("����")]
    GameObject[] clothesButton;

    int danchuIndex = 3;    // ���� ��� ����
    int clothesButtonItemCount = 0;    // �ֿ� ���� ����

    Vector3 enemyVector = Vector3.zero;    // Enemy�� ��ġ

    EventParam eventParam = new EventParam();

    private void Start()
    {
        // �׾��� �� �̺�Ʈ �ޱ� ( ���� ������ )
        clothesButtonItemCount = 0;
        EventManager.StartListening("DEAD", DropClothesButton);
    }
    private void OnDestroy()
    {
        EventManager.StopListening("DEAD", DropClothesButton);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))   // ���� ������ ���
        {
            UseItem();
        }
    }

    // ������ ���
    protected override void UseItem()
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
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("BOSS"))
        {
            enemyVector = collision.transform.position; // ���� ��ġ �ޱ�
        }
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
        Vector3 buttonPos = (transform.position - enemyVector).normalized;
        if (danchuIndex % 2 == 0) // ���߰� ���ɰ����� ����
        {
            danchuIndex = danchuIndex / 2 - 1;
        }
        else // ���߰� ���ɰ����� �ִ�
        {
            danchuIndex = (danchuIndex - 1) / 2 - 1;
        }
        clothesButton[danchuIndex].transform.localPosition = new Vector3(buttonPos.x * buttonDistance, 0.2f, buttonPos.z * buttonDistance);
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
        baseWeapon.SetActive(true);
        item.SetActive(false);
        isUsing = false;
    }
}
