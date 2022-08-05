using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VFXSet : MonoSingleton<VFXSet>
{
    private static AudioSource VFXaudioSource;

    public AudioMixer audioMixer;

    public AudioSource PlayerSource;
    public AudioClip[] playerAudioClip;
    private void Awake()
	{
        VFXaudioSource = GetComponent<AudioSource>();
	}
	private void Start()
    {
        EventManager.StartListening("Start", StartBM);
    }
    private void Update()
    {
        audioMixer.SetFloat("Effect", UIManager.Instance.GetBgmVolume());
        float a;
        audioMixer.GetFloat("Effect", out a);
        if (a == -40f)
            audioMixer.SetFloat("Effect", -80f);
    }

    public void StopPlayerEffect()
    {
        PlayerSource.Stop();
    }

    public void StartBM(EventParam eventParam)
    {
        PlayerVFXSet((int)PlayerVFXs.Attack);
    }

    public void PlayerVFXSet(int a)
    {
        PlayerSource.Stop();
        PlayerSource.clip = playerAudioClip[a];
        PlayerSource.Play();
    }
}

public enum PlayerVFXs
{
    Walk,
    Run,
    Attack,
    Hit,
    ButtonCrush,
    Stomp
}
