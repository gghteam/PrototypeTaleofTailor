using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemManager : MonoBehaviour
{
    [SerializeField, Header("아이템")]
    protected GameObject item;
    [SerializeField, Header("사용 시간")]
    protected int useTime;
    [SerializeField, Header("기본 가위 무기")]
    protected GameObject baseWeapon;

    protected static bool isUsing;
    // 아이템 사용 시
    protected abstract void UseItem();

    // 아이템 얻었을 때
    protected abstract void GetItem();

    // 아이템 갯수가 0일 때
    protected abstract void ItemZero();

}
