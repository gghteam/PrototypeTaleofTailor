using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedleItem : ItemManager
{
    [SerializeField, Header("바늘 아이템 갯수")]
    int needleCount;
    [SerializeField, Header("바늘 아이템 공격력 증가량")]
    int attackPower = 30;


    EventParam eventParam = new EventParam();


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            UseItem();
        }
    }

    protected override void GetItem()
    {
        // 프로토 타입에는 없는 시스템
    }

    protected override void ItemZero()
    {
        eventParam.boolParam = false;
        eventParam.itemParam = Item.NEEDLE;
        EventManager.TriggerEvent("ITEMHAVE", eventParam);
    }

    protected override void UseItem()
    {
        if (isUsing) return;
        if (needleCount > 0)
        {
            needleCount--;
        }
        if (needleCount <= 0)
        {
            ItemZero();
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
        isUsing = true;
        item.SetActive(true);
        baseWeapon.SetActive(false);
        eventParam.itemParam = Item.NEEDLE;
        EventManager.TriggerEvent("ITEMUSEANIM", eventParam);
        Invoke("NeedleStop", useTime);
    }

    void NeedleStop()
    {
        eventParam.itemParam = Item.NEEDLE;
        EventManager.TriggerEvent("ITEMSTOPANIM", eventParam);
        baseWeapon.SetActive(true);
        item.SetActive(false);
        isUsing = false;
    }

}
