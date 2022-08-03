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

    Animator anim;
    private readonly int hashDrink = Animator.StringToHash("Drinking");

    Vector3 savePoint = Vector3.zero;
    bool isRest = false;

    EventParam eventParam = new EventParam();

    private void Awake()
    {
        anim = GetComponent<Animator>();

        savePoint = defaulPosition.position;
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
