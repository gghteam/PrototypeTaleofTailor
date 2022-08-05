using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundSet : MonoBehaviour
{
    [SerializeField]
    private AudioSource BGMaudioSource;

    public AudioMixer audioMixer;
	private void Update()
    {
        audioMixer.SetFloat("Music", UIManager.Instance.GetBgmVolume());
        float a;
        audioMixer.GetFloat("Music", out a);
        if (a == -40f)
            audioMixer.SetFloat("Music", -80f);
    }

    public void SrartBGM()
    {
        BGMaudioSource.Play();
    }
}
