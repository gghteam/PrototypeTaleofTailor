using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Inventory : MonoBehaviour
{
    [Header("�κ��丮 â")]
    [SerializeField]
    GameObject inventoryPanel;    // ��ü���� �κ��丮 â
    [SerializeField]
    GameObject[] invenTab;    // �������� �κ��丮 ���� ���� â 

    [SerializeField, Header("���õ� â ǥ�� �̹���")]
    Image selectImage;

    [Header("�ΰ��� ������ ǥ�� UI")]
    [SerializeField]
    Image[] itemImage;    // ������ â �������̹���
    // ������ ���� ǥ�� TEXT

    [SerializeField]
    Text[] itemText;
    [SerializeField]
    GameObject[] itemTextB;
    //Image beforeItem;

    public NewBehaviourScript textData;


    EventParam eventParam = new EventParam();

    private void Start()
    {
        Instantiate(itemText[0], itemTextB[0].transform);
        Instantiate(itemText[1], itemTextB[1].transform);
        Instantiate(itemText[2], itemTextB[2].transform);

        EventManager.StartListening("ITEMHAVE", ItemHave);      // ������ �������� �Ǵ� �� ������ �κ� ���� Ű��
        EventManager.StartListening("ITEMTEXT", ItemTextIndex);    // ������ ���� �ؽ�Ʈ ǥ��
    }

    // ������ ���� �ؽ�Ʈ�� ǥ��
    void ItemTextIndex(EventParam eventParam)
    {
        itemText[(int)eventParam.itemParam].text = string.Format($"{eventParam.intParam}");
    }

    private void Update()
    {
        SettingTabOnOff();  // ESC�� �κ�â ���� Ű��
    }

    // �κ��丮 ���� â �ٲٱ�
    public void InvenOpen(int tab)
    {
        for (int i = 0; i < 4; i++)
        {
            if (i == tab)
            {
                //����â ǥ��
                selectImage.rectTransform.anchoredPosition = new Vector3(355 * (i + 1), -59f, 0f);
                UIManager.Instance.UiOpen(invenTab[i]);

            }
            else
                UIManager.Instance.UiClose(invenTab[i]);
        }
    }

    //  ESC�� �κ��丮 ���� Ű��
    private void SettingTabOnOff()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (UIManager.Instance.isSetting)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                UIManager.Instance.UiClose(inventoryPanel);
            }
            else
            {
                UIManager.Instance.UiOpen(inventoryPanel);
                //Ŀ�� ����� �� ��ġ ����
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            UIManager.Instance.isSetting = !UIManager.Instance.isSetting;
        }
    }

    // �ΰ��� ������ â �������̹��� Ű�� ����
    private void ItemImageOn(Item item, bool isOpen)
    {
        Image obj = itemImage[(int)item];
        if (isOpen)
        {
            obj.gameObject.SetActive(true);
        }
        else
        {
            obj.gameObject.SetActive(false);
        }
    }

    // ������ â ���� Ű�� �Լ� ���� + ���� ����
    void ItemHave(EventParam eventParam)
    {
        ItemImageOn(eventParam.itemParam, eventParam.boolParam);
    }
}
