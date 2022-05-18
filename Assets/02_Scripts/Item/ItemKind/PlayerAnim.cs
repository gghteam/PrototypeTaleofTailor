using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �÷��̾� ��ũ��Ʈ ��� ����
/// </summary>
public class PlayerAnim : Character
{
    EventParam eventParam = new EventParam();
    private bool isItem = false;
    private readonly int hashItem = Animator.StringToHash("IsItem");
    private readonly int hashItemIndex = Animator.StringToHash("ItemAnim");

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
        EventManager.StopListening("ITEMUSEANIM", ItemUseAnim);
    }

    void IsUsingItem()
    {
        eventParam.boolParam = !isItem;
        EventManager.TriggerEvent("ISMOVE", eventParam);
    }

    // �ϴ� HoldItem �ϳ��� ��ü
    void ItemUseAnim(EventParam eventParam)
    {
        isItem = true;
        IsUsingItem();
        ani.SetInteger(hashItemIndex, (int)eventParam.itemParam);
        ani.SetBool(hashItem, isItem);
    }

    public void IsItemChange(int value)
    {
        isItem = value == 0 ? false : true;
        IsUsingItem();
        ani.SetBool(hashItem, isItem);
    }


}
