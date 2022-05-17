using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 플레이어 스크립트 대신 만듦
/// </summary>
public class PlayerAnim : Character
{
    EventParam eventParam = new EventParam();
    private void Awake()
    {
        ani = GetComponent<Animator>();
    }
    private void Start()
    {
        EventManager.StartListening("ITEMUSEANIM", ItemUseAnim);
        EventManager.StartListening("ITEMSTOPANIM", ItemStopAnim);
    }
    private void OnDestroy()
    {
        EventManager.StopListening("ITEMSTOPANIM", ItemUseAnim);
        EventManager.StopListening("ITEMUSEANIM", ItemStopAnim);
    }

    // 일단 HoldItem 하나로 대체
    void ItemUseAnim(EventParam eventParam)
    {
        switch (eventParam.itemParam)
        {
            case Item.NEEDLE:
                {
                    ani.SetBool("IsNeedleUse", true);
                }
                break;
            case Item.BANDAGE:
                {
                    ani.SetBool("IsBandageUse", true);
                }
                break;
            case Item.CLOTHES_BUTTON:
                {
                    ani.SetBool("IsClothesButtonUse", true);
                }
                break;
        }
    }

    void ItemStopAnim(EventParam eventParam)
    {
        switch (eventParam.itemParam)
        {
            case Item.NEEDLE:
                {
                    ani.SetBool("IsNeedleUse", false);
                }
                break;
            case Item.BANDAGE:
                {
                    ani.SetBool("IsBandageUse", false);
                }
                break;
            case Item.CLOTHES_BUTTON:
                {
                    ani.SetBool("IsClothesButtonUse", false);
                }
                break;
        }
    }


}
