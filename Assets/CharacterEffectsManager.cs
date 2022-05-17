using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEffectsManager : MonoBehaviour
{
    protected WeaponFX rightWeaponFX;
    protected WeaponFX leftWeaponFX;
    public virtual void PlayWeaponFX(bool isLeft)
    {
        if(isLeft == false)
        {
            //PLAY THE RIGHT WEAPONS TRAI
            if(rightWeaponFX != null)
            {
                rightWeaponFX.PlayWeaponFX();
            }
        }
        else
        {
            //PLAY THE LEFT WEAPONS TRAIL
            if(leftWeaponFX != null)
            {
                leftWeaponFX.PlayWeaponFX();
            }
        }
    }
}
