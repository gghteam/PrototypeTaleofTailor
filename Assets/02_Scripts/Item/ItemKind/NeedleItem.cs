using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedleItem : ItemManager
{
    [SerializeField, Header("�ٴ� ������ ����")]
    int needleCount;
    [SerializeField, Header("�ٴ� ������ ���ݷ� ������")]
    int attackPower = 30;


    EventParam eventParam = new EventParam();


    private void Awake()
    {
        EventManager.StartListening("NeedleItem", UseItem);
    }
    private void OnDestroy()
    {
        EventManager.StopListening("NeedleItem", UseItem);
    }

    protected override void GetItem()
    {
        // ������ Ÿ�Կ��� ���� �ý���
    }

    protected override void ItemZero()
    {
        eventParam.boolParam = false;
        eventParam.itemParam = Item.NEEDLE;
        EventManager.TriggerEvent("ITEMHAVE", eventParam);
    }

    protected override void UseItem(EventParam eventParam)
    {
        if (isUsing) return;
        if (needleCount <= 0) return;
        if (needleCount > 0)
        {
            isUsing = true;
            needleCount--;
            if(needleCount <= 0)
            {
                ItemZero();
            }
        }
        eventParam.itemParam = Item.NEEDLE;
        eventParam.intParam = needleCount;
        EventManager.TriggerEvent("ITEMTEXT", eventParam);
        eventParam.intParam = attackPower;
        EventManager.TriggerEvent("PLUS_ATTACKPOWER", eventParam);
        NeedleUseAnim();
    }

    void NeedleUseAnim()
    {
        item.SetActive(true);
        baseWeapon.SetActive(false);
        eventParam.itemParam = Item.NEEDLE;
        eventParam.boolParam= false;
        EventManager.TriggerEvent("ITEMUSEANIM", eventParam);
        Invoke("NeedleStop", useTime);
    }

    void NeedleStop()
    {
        eventParam.boolParam = false;
        EventManager.TriggerEvent("ITEMOFF", eventParam);
        baseWeapon.SetActive(true);
        item.SetActive(false);
        isUsing = false;
    }

}
