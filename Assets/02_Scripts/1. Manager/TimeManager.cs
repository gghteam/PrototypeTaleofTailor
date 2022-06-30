using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoSingleton<TimeManager>
{
    public void Awake()
    {
        ResetTimeScale();
    }

    public void ResetTimeScale()
    {
        StopAllCoroutines();
        Time.timeScale = 1f;
    }

    public void ModifyTimeScale(float endValue, float waitTime, Action Callback = null)
    {
        StartCoroutine(ModifyTimeScaleCoroutine(endValue, waitTime, Callback));
    }

    private IEnumerator ModifyTimeScaleCoroutine(float endValue, float waitTime, Action Callback = null)
    {
        yield return new WaitForSeconds(waitTime);
        Time.timeScale = endValue;
        Callback?.Invoke();
    }
}
