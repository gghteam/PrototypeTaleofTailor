using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombAttack : FsmState
{
    [SerializeField, Header("¸ñÇ¥ ÁöÁ¡")]
    Transform target;
    [SerializeField, Header("ÃÑ¾Ë ½ºÆù À§Ä¡")]
    Transform bombPos;

    [SerializeField, Header("ÆøÅº Prefab")]
    GameObject bomb;

    Animation anim;

    private void Awake()
    {
        anim = GetComponent<Animation>();
    }
    private void Update()
    {
        LookPlayer();
    }

    public override void OnStateEnter()
    {
        InvokeRepeating("Attack", 0.1f, 5f);
    }
    public override void OnStateLeave()
    {
        CancelInvoke();
        this.GetComponent<BombAttack>().enabled = false;
    }

    // ÆøÅº »ý¼º
    void Attack()
    {
        anim.CrossFade("Armature_soldier_B_reloading", 0.25f);
        Invoke("Shoot", 3.5f);
    }

    void Shoot()
    {
        anim.CrossFade("Armature_soldier_B_shoot", 0.25f);
        GameObject _bullet = Instantiate(bomb, bombPos.position, transform.rotation);
    }

    void LookPlayer()
    {
        Vector3 vec = target.position-transform.position;
        vec.Normalize();
        Quaternion q = Quaternion.LookRotation(vec);
        transform.rotation = q;
    }


}
