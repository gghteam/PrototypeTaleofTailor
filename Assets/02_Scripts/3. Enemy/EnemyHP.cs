using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    [SerializeField]
    int hp;

    FsmLegacyAni anim;

    //
    MonsterDying dieState;
    EnemyKilled killed;
    Collider collider;

    private void Awake()
    {
        anim = GetComponent<FsmLegacyAni>();
        dieState = GetComponent<MonsterDying>();
        collider = GetComponent<Collider>();
        killed = GetComponent<EnemyKilled>();
    }

    public void Damage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            // Á×À½
            killed.Dead = true;
        }
        else DamageEffect();
    }


    void DamageEffect()
    {
        anim.ChangeAnimation(FsmLegacyAni.ClipState.Damage, 0.25f);
    }
}
