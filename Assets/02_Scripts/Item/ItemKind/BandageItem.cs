using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BandageItem : ItemManager
{
    [SerializeField, Header("붕대 아이템 갯수")]
    int bandageCount;
    [SerializeField, Header("붕대 아이템의 스테미나 증가량")]
    int plusStemina = 1;

    EventParam eventParam = new EventParam();


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            UseItem();
        }
    }

    protected override void GetItem()
    {

    }

    protected override void UseItem()
    {
        if (isUsing) return;
        if (bandageCount > 0)
        {
            isUsing = true;
            bandageCount--;
        }
        if (bandageCount <= 0)
        {
            ItemZero();
        }
        eventParam.itemParam = Item.BANDAGE;
        eventParam.intParam = bandageCount;
        EventManager.TriggerEvent("ITEMTEXT", eventParam);
        SteminaManager.Instance.PlusStemina(plusStemina);
        BandageUseAnim();
    }

    protected override void ItemZero()
    {
        eventParam.boolParam = false;
        eventParam.itemParam = Item.BANDAGE;
        EventManager.TriggerEvent("ITEMHAVE", eventParam);
    }
    void BandageUseAnim()
    {
        item.SetActive(true);
        baseWeapon.SetActive(false);
        eventParam.itemParam = Item.BANDAGE;
        EventManager.TriggerEvent("ITEMUSEANIM", eventParam);
        Invoke("BandageStop", useTime);
    }
    void BandageStop()
    {
        eventParam.boolParam = false;
        EventManager.TriggerEvent("ITEMOFF", eventParam);
        baseWeapon.SetActive(true);
        item.SetActive(false);
        isUsing = false;
    }
}
