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
        // ���� ����Ʈ
    }
    void EffectSave()
    {
        // ���� ���� ����Ʈ
        // �÷��̾� �� ���ô� �ִϸ��̼� �߰�
    }

}
