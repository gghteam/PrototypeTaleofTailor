using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFX : MonoBehaviour
{
    [Header("weapon FX")]
    public ParticleSystem normalweaponTrail;

    public void PlayWeaponFX()
    {
        normalweaponTrail.Stop();

        if(normalweaponTrail.isStopped)
        {
            normalweaponTrail.Play();
        }
    }
}
