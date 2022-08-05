using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float hp = 0;
    private void Start()
    {
        EventManager.StartListening("ENEMYDAMAGE", Damaged);
    }
    private void Update()
    {

    }
    private void Damaged(EventParam eventParam)
    {
        hp -= eventParam.intParam;
    }

    private void OnDestroy()
	{
        EventManager.StopListening("ENEMYDAMAGE", Damaged);
    }
}
