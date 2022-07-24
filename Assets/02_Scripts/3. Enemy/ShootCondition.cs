using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootCondition : FsmState
{
    private FsmLegacyAni fsmLegacyAni;
    //[SerializeField]
    private GameObject player;

    public float cooldown;

    private float cntdwn;

    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private Transform bulletPos;
    [SerializeField]
    private ParticleSystem muzzleFlash;

    private void Awake()
    {
        fsmLegacyAni = GetComponent<FsmLegacyAni>();
        cntdwn = 0;
    }

    private void Start()
    {
        player = Define.Player;
    }

    private void Update()
    {
        transform.LookAt(player.transform);

        if((cntdwn -= Time.deltaTime) <= 0)
        {
            Debug.Log("Attack");
            cntdwn += cooldown;


            Vector3 l_vector = player.transform.position - transform.position;
            Quaternion qur = Quaternion.LookRotation(l_vector).normalized;
            qur = Quaternion.Euler(90, qur.eulerAngles.y, 0);

            fsmLegacyAni.ChangeAnimation(FsmLegacyAni.ClipState.Attack, 0.3f);
            muzzleFlash.Emit(30);
            Instantiate(bullet, bulletPos.position, qur);
        }
    }
}
