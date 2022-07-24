using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombDying : FsmState
{
    Animation anim;

    private void Awake()
    {
        anim = GetComponent<Animation>();
    }

    public override void OnStateEnter()
    {
        anim.CrossFade("Armature_soldier_B_dead", 0.25f);
        Invoke("Die", 2f);
    }

    void Die()
    {
        gameObject.SetActive(false);
    }


}
