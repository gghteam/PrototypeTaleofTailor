using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Inventory : MonoBehaviour
{
    [Header("인벤토리 창")]
    [SerializeField]
    GameObject inventoryPanel;    // 전체적인 인벤토리 창
    [SerializeField]
    GameObject[] invenTab;    // 세부적인 인벤토리 내에 나뉜 창 

    [SerializeField, Header("선택된 창 표시 이미지")]
    Image selectImage;

    [Header("인게임 아이템 표시 UI")]
    [SerializeField]
    Image[] itemImage;    // 아이템 창 아이템이미지
    // 아이템 갯수 표시 TEXT

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

        EventManager.StartListening("ITEMHAVE", ItemHave);      // 아이템 가졌는지 판단 후 아이템 인벤 끄고 키기
        EventManager.StartListening("ITEMTEXT", ItemTextIndex);    // 아이템 갯수 텍스트 표시
    }

    // 아이템 갯수 텍스트로 표시
    void ItemTextIndex(EventParam eventParam)
    {
        itemText[(int)eventParam.itemParam].text = string.Format($"{eventParam.intParam}");
    }

    private void Update()
    {
        SettingTabOnOff();  // ESC로 인벤창 끄고 키기
    }

    // 인벤토리 내에 창 바꾸기
    public void InvenOpen(int tab)
    {
        for (int i = 0; i < 4; i++)
        {
            if (i == tab)
            {
                //선택창 표시
                selectImage.rectTransform.anchoredPosition = new Vector3(355 * (i + 1), -59f, 0f);
                UIManager.Instance.UiOpen(invenTab[i]);

            }
            else
                UIManager.Instance.UiClose(invenTab[i]);
        }
    }

    //  ESC로 인벤토리 끄고 키기
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
                //커서 숨기기 및 위치 해제
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            UIManager.Instance.isSetting = !UIManager.Instance.isSetting;
        }
    }

    // 인게임 아이템 창 아이템이미지 키고 끄기
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

    // 아이템 창 끄고 키기 함수 실행 + 갯수 띄우기
    void ItemHave(EventParam eventParam)
    {
        ItemImageOn(eventParam.itemParam, eventParam.boolParam);
    }
}
