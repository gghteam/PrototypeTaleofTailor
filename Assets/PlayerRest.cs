using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRest : MonoBehaviour
{
    Animator anim;

    Vector3 savePoint = Vector3.zero;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        EventManager.StartListening("Rest", Resting);
        EventManager.StartListening("Spawn", Spawn);
    }

    private void OnDestroy()
    {
        EventManager.StopListening("Rest", Resting);
        EventManager.StopListening("Spawn", Spawn);
    }

    void Resting(EventParam eventParam)
    {
        EffectSave();
        savePoint = transform.position;
    }

    void Spawn(EventParam eventParam)
    {
        EffectSpawn();
        transform.position = savePoint;
    }

    void EffectSpawn()
    {
        // 스폰 이펙트
    }
    void EffectSave()
    {
        // 스폰 저장 이펙트
        // 플레이어 차 마시는 애니메이션 추가
    }

}
