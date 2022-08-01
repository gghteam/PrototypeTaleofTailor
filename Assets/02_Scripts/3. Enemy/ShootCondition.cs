using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootCondition : FsmState
{
    private FsmLegacyAni fsmLegacyAni;
    EnemyKilled killed;
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
    Quaternion qur;

    private bool isShoot = false;

    private void Awake()
    {
        fsmLegacyAni = GetComponent<FsmLegacyAni>();
        killed = GetComponent<EnemyKilled>();
        cntdwn = 0;
    }

    private void Start()
    {
        player = Define.Player;
    }

    private void Update()
    {
        if (isShoot) return;

        transform.LookAt(player.transform);

        if((cntdwn -= Time.deltaTime) <= 0)
        {
            Debug.Log("Attack");

            cntdwn += cooldown;

            Vector3 l_vector = player.transform.position - transform.position;
            qur = Quaternion.LookRotation(l_vector).normalized;
            qur = Quaternion.Euler(90, qur.eulerAngles.y, 0);

            isShoot = true;
            Reloading();
        }
    }

    void Reloading()
    {
        fsmLegacyAni.ChangeAnimation(FsmLegacyAni.ClipState.Reloading, 0.25f);
        Invoke("Shoot", 3f);
    }

    void Shoot()
    {
        if (killed.Dead) return;
        fsmLegacyAni.ChangeAnimation(FsmLegacyAni.ClipState.Attack, 0.25f);
        muzzleFlash.Emit(30);
        Instantiate(bullet, bulletPos.position, qur);
        isShoot = false;
    }

    public override void OnStateLeave()
    {
        CancelInvoke("Shoot");
    }
}
