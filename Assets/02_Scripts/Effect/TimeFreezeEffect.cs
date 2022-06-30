using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeFreezeEffect : MonoBehaviour
{
    [SerializeField, Tooltip("어는데 걸리는 시간")]
    private float freezeTimeDelay = 0.05f;
    [SerializeField, Tooltip("언것이 풀리는데 걸리는 시간")]
    private float unfreezeTimeDalay = 0.02f;

    [SerializeField, Tooltip("시간을 얼마만큼 느리게 할 건지?")]
    private float timeFreezeValue = 0.2f;

    public void TimeFreeze()
    {
        Debug.Log("시간 느려짐");
        TimeManager.Instance.ModifyTimeScale(timeFreezeValue, freezeTimeDelay, () =>
        {
            Debug.Log("시간 빨라짐");
            TimeManager.Instance.ModifyTimeScale(1, unfreezeTimeDalay);
        });
    }
}
