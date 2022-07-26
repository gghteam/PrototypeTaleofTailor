using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FsmLegacyAni : MonoBehaviour
{
    private Animation animation = null;

    // 0:Idle 1:Move 2:Attack
    public List<AnimationClip> clips;

    public enum ClipState
    {
        Idle,
        Move,
        Attack,
        Reloading,
        Damage,
        Dead
    }


    private void Awake()
    {
        animation = GetComponent<Animation>();
    }

    public void ChangeAnimation(ClipState curr, float time)
    {
        animation.CrossFade(clips[(int)curr].name, time);
    }
}
