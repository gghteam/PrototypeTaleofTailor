using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundManager : MonoSingleton<SoundManager>
{
    AudioSource[] _audioSources = new AudioSource[(int)Sound.MAXCOUNT];

    [SerializeField, Header("BGM AuidoSource")]
    AudioSource bgmSource;
    [SerializeField, Header("SFX AuidoSource")]
    AudioSource sfxSource;
    Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();


    float bgmVolume = 1;
    float sfxVolume = 1;

    void Start()
    {
        bgmVolume = PlayerPrefs.GetFloat("BGMVOLUME", 1);
        sfxVolume = PlayerPrefs.GetFloat("SFXVOLUME", 1);
    }
    void Update()
    {
        bgmVolume = UIManager.Instance.GetBgmVolume();
        sfxVolume = UIManager.Instance.GetSfxVolume();
        UpdateVolume();
    }

    void UpdateVolume()
    {
        bgmSource.volume = bgmVolume;
        sfxSource.volume = sfxVolume;

        PlayerPrefs.SetFloat("BGMVOLUME", bgmVolume);
        PlayerPrefs.SetFloat("SFXVOLUME", sfxVolume);
    }
}
