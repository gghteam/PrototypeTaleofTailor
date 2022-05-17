using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] Animator _Anim;
    [SerializeField] CrackControll _CrackPrefab;
    Vector3 direction;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            //AnimationCallback_SlamEffect();
           _Anim.SetTrigger("Attack");
        }
    }


    
    private void AnimationCallback_SlamEffect()
    {
        direction = transform.forward;
        Debug.Log("¡¢±Ÿ¡ﬂ");
        Vector3 pos = transform.position * 1.3f;
        pos.y = 0;
        CrackControll crackControll = Instantiate(_CrackPrefab, pos, Quaternion.identity);
        crackControll.transform.forward = direction;
        crackControll.Open(15);
    }
    
    
}
