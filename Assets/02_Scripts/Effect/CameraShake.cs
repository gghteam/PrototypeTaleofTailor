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

    public void CameraShakeEffect(EventParam eventParam)
    {
        StartCoroutine(ShakeCoroutine());
    }

    private IEnumerator ShakeCoroutine()
    {
        noise1.m_AmplitudeGain = shakeValue;
        noise2.m_AmplitudeGain = shakeValue;
        noise3.m_AmplitudeGain = shakeValue;
        yield return new WaitForSeconds(shakeTime);
        noise1.m_AmplitudeGain = 0;
        noise2.m_AmplitudeGain = 0;
        noise3.m_AmplitudeGain = 0;
    }
}
