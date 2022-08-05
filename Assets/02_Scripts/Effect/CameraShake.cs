using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using static Define;
using System;

public class CameraShake : MonoBehaviour
{
    [SerializeField, Tooltip("Èçµé¸² Á¤µµ")]
    private float shakeValue = 1f;
    [SerializeField, Tooltip("Èçµé¸± ½Ã°£")]
    private float shakeTime = 0.3f;

    private CinemachineBasicMultiChannelPerlin noise1;
    private CinemachineBasicMultiChannelPerlin noise2;
    private CinemachineBasicMultiChannelPerlin noise3;

    private void Start()
    {
        noise1 = FLCam.GetRig(0).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        noise2 = FLCam.GetRig(1).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        noise3 = FLCam.GetRig(2).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        EventManager.StartListening("CameraShake", CameraShakeEffect);
    }

    private void OnDestroy()
    {
        EventManager.StopListening("CameraShake", CameraShakeEffect);
    }

    public void CameraShakeEffect(EventParam eventParam)
    {
        StartCoroutine(ShakeCoroutine(eventParam.floatParam, eventParam.floatParam2));
    }

    private IEnumerator ShakeCoroutine(float value, float time)
    {
        noise1.m_AmplitudeGain = value;
        noise2.m_AmplitudeGain = value;
        noise3.m_AmplitudeGain = value;
        yield return new WaitForSeconds(time);
        noise1.m_AmplitudeGain = 0;
        noise2.m_AmplitudeGain = 0;
        noise3.m_AmplitudeGain = 0;
    }
}
