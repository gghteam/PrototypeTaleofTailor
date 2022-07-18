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
    private readonly int hashItem = Animator.StringToHash("IsItem");
    private readonly int hashItemIndex = Animator.StringToHash("ItemIndex");

    protected override void Awake()
    {
        base.Awake();
        ani = GetComponent<Animator>();
    }
    private void Start()
    {
        EventManager.StartListening("ITEMUSEANIM", ItemUseAnim);
        EventManager.StartListening("ITEMOFF", IsItemChange);
    }
    private void OnDestroy()
    {
        EventManager.StopListening("ITEMUSEANIM", ItemUseAnim);
        EventManager.StopListening("ITEMOFF", IsItemChange);
    }

    void IsUsingItem()
    {
        eventParam.boolParam = !isItem;
        EventManager.TriggerEvent("ISMOVE", eventParam);
    }

    void ItemUseAnim(EventParam eventParam)
    {
        isItem = true;
        IsUsingItem();
        ani.SetFloat(hashItemIndex, (float)eventParam.itemParam);
        ani.SetBool(hashItem, isItem);
    }

    public void IsItemChange(EventParam eventParam)
    {
        isItem = eventParam.boolParam;
        IsUsingItem();
        ani.SetBool(hashItem, isItem);
    }


}
