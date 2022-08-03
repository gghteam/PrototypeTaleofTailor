using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRest : MonoBehaviour
{
    [SerializeField]
    ParticleSystem spawnParticle;
    [SerializeField]
    GameObject cup;
    [SerializeField]
    Transform defaulPosition;
    [SerializeField]
    private Transform bossPos;

    Animator anim;
    private readonly int hashDrink = Animator.StringToHash("Drinking");

    Vector3 savePoint = Vector3.zero;
    bool isRest = false;

    EventParam eventParam = new EventParam();
    private StageInfo stageInfo;

    private void Start()
    {
        anim = GetComponent<Animator>();

        stageInfo = Gamemanager.Instance.LoadJsonFile<StageInfo>(Gamemanager.Instance.SavePath, Gamemanager.Instance.SaveFileName);
        savePoint = stageInfo.pos;
        if (stageInfo.isBoos == true)
        {
            savePoint = bossPos.position;
            Gamemanager.Instance.SaveJson<StageInfo>(Gamemanager.Instance.SavePath, Gamemanager.Instance.SaveFileName, stageInfo);
        }
        else
        {
            if (savePoint == Vector3.zero)
            {
                savePoint = defaulPosition.position;
                Gamemanager.Instance.SaveJson<StageInfo>(Gamemanager.Instance.SavePath, Gamemanager.Instance.SaveFileName, stageInfo);
            }
        }

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
        Gamemanager.Instance.SaveJson<StageInfo>(Gamemanager.Instance.SavePath, Gamemanager.Instance.SaveFileName, stageInfo);
    }

    void Spawn(EventParam eventParam)
    {
        EffectSpawn();
        stageInfo = Gamemanager.Instance.LoadJsonFile<StageInfo>(Gamemanager.Instance.SavePath, Gamemanager.Instance.SaveFileName);
        transform.position = savePoint;
    }

    void EffectSpawn()
    {
        // 스폰 이펙트
        spawnParticle.Play();
    }
    void EffectSave()
    {
        if (isRest) return;
        isRest = true;
        // 스폰 저장 이펙트
        // 플레이어 차 마시는 애니메이션 추가
        cup.SetActive(true);
        MoveStop();
        anim.SetTrigger(hashDrink);
        Invoke("MoveStart", 5f);
        //spawnParticle.Play();
    }

    void MoveStop()
    {
        eventParam.boolParam = false;
        EventManager.TriggerEvent("ISMOVE", eventParam);
    }
    
    void MoveStart()
    {
        isRest = false;
        cup.SetActive(false);
        eventParam.boolParam = true;
        EventManager.TriggerEvent("ISMOVE", eventParam);
    }

}
