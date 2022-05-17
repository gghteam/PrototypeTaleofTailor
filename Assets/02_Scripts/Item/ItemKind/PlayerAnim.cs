using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 플레이어 스크립트 대신 만듦
/// </summary>
public class PlayerAnim : Character
{
    EventParam eventParam = new EventParam();
    private bool isItem = false;

    private void Awake()
    {
        ani = GetComponent<Animator>();
    }
    private void Start()
    {
        EventManager.StartListening("ITEMUSEANIM", ItemUseAnim);
    }
    private void OnDestroy()
    {
        EventManager.StopListening("ITEMSTOPANIM", ItemUseAnim);
    }

    // 일단 HoldItem 하나로 대체
    void ItemUseAnim(EventParam eventParam)
    {
        isItem = true;
        ani.SetBool("IsItem", isItem);
        ani.SetInteger("ItemAnim", (int)eventParam.itemParam);
    }

    public void IsItemChange(int value)
    {
        isItem = value == 0 ? false : true;
        ani.SetBool("IsItem", isItem);
    }


}
