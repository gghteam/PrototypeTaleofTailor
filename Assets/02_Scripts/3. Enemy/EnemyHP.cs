using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    [SerializeField]
    int hp;

    FsmLegacyAni anim;

    //
    BombDying dieState;
    BombKilled killed;
    

    private void Awake()
    {
        anim = GetComponent<FsmLegacyAni>();
        dieState = GetComponent<BombDying>();
        killed = GetComponent<BombKilled>();
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
