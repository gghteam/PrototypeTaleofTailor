using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
    [SerializeField]
    GameObject bossCutsceneCol;

    public UnityEvent OnBossAction;
    public UnityEvent OnNotBossAction;

    Animator anim;
    private readonly int hashDrink = Animator.StringToHash("Drinking");

    Vector3 savePoint = Vector3.zero;
    bool isRest = false;

    EventParam eventParam = new EventParam();
    private StageInfo stageInfo;

    [SerializeField]
    private FsmCore bossFsmCore;
    [SerializeField]
    private EnemyIdle bossIdle;
    [SerializeField]
    private GameObject boss;
    [SerializeField]
    private Transform bossDefaulPos;


    private void Start()
    {
        anim = GetComponent<Animator>();

        stageInfo = Gamemanager.Instance.LoadJsonFile<StageInfo>(Gamemanager.Instance.SavePath, Gamemanager.Instance.SaveFileName);
        savePoint = stageInfo.pos;
        if (stageInfo.isBoss == true)
        {
            savePoint = bossPos.position;
            transform.position = savePoint;
            stageInfo.isBoss = false;
            Gamemanager.Instance.SaveJson<StageInfo>(Gamemanager.Instance.SavePath, Gamemanager.Instance.SaveFileName, stageInfo);
            bossCutsceneCol.gameObject.SetActive(false);
            OnBossAction?.Invoke();
        }
        else
        {
            if (savePoint == Vector3.zero)
            {
                savePoint = defaulPosition.position;
                transform.position = savePoint;
                Gamemanager.Instance.SaveJson<StageInfo>(Gamemanager.Instance.SavePath, Gamemanager.Instance.SaveFileName, stageInfo);
            }
            else
            {
                transform.position = savePoint;
            }
            bossCutsceneCol.gameObject.SetActive(true);
            OnNotBossAction?.Invoke();
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
        stageInfo.pos = savePoint;
        Gamemanager.Instance.SaveJson<StageInfo>(Gamemanager.Instance.SavePath, Gamemanager.Instance.SaveFileName, stageInfo);
    }

    void Spawn(EventParam eventParam)
    {
        bossFsmCore.ChangeState(bossIdle);
        boss.transform.position = bossDefaulPos.position;

        EffectSpawn();
        stageInfo = Gamemanager.Instance.LoadJsonFile<StageInfo>(Gamemanager.Instance.SavePath, Gamemanager.Instance.SaveFileName);
        savePoint = stageInfo.pos;
        if(savePoint == Vector3.zero)
        {
            savePoint = defaulPosition.position;
            Gamemanager.Instance.SaveJson<StageInfo>(Gamemanager.Instance.SavePath, Gamemanager.Instance.SaveFileName, stageInfo);
        }
        transform.position = savePoint;
    }

    void EffectSpawn()
    {
        // ���� ����Ʈ
        spawnParticle.Play();
    }
    void EffectSave()
    {
        if (isRest) return;
        isRest = true;
        // ���� ���� ����Ʈ
        // �÷��̾� �� ���ô� �ִϸ��̼� �߰�
        cup.SetActive(true);
        MoveStop();
        anim.SetTrigger(hashDrink);
        StartCoroutine(Effect());
        //spawnParticle.Play();
    }

    IEnumerator Effect()
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(5f);
        Time.timeScale = 1f;
        MoveStart();
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
