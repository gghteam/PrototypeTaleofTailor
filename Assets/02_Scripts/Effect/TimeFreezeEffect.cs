using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeFreezeEffect : MonoBehaviour
{
    [SerializeField, Tooltip("��µ� �ɸ��� �ð�")]
    private float freezeTimeDelay = 0.05f;
    [SerializeField, Tooltip("����� Ǯ���µ� �ɸ��� �ð�")]
    private float unfreezeTimeDalay = 0.02f;

    [SerializeField, Tooltip("�ð��� �󸶸�ŭ ������ �� ����?")]
    private float timeFreezeValue = 0.2f;

    public void TimeFreeze()
    {
        Debug.Log("�ð� ������");
        TimeManager.Instance.ModifyTimeScale(timeFreezeValue, freezeTimeDelay, () =>
        {
            Debug.Log("�ð� ������");
            TimeManager.Instance.ModifyTimeScale(1, unfreezeTimeDalay);
        });
    }
}
