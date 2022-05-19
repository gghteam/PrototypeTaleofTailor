using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXSet : MonoBehaviour
{
    [SerializeField]
    private AudioSource VFXaudioSource;

    private void Start()
    {
        EventManager.StartListening("Start", StartBM);
    }
    private void Update()
    {
        VFXaudioSource.volume = UIManager.Instance.GetSfxVolume();
    }

    public void SrartBGM()
    {
        VFXaudioSource.Play();
    }

    public void StartBM(EventParam eventParam)
    {
        VFXaudioSource.Play();
    }
}
