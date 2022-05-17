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

    // �ϴ� HoldItem �ϳ��� ��ü
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
