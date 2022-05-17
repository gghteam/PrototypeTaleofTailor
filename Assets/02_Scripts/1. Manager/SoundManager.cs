using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    AudioSource[] _audioSources = new AudioSource[(int)Sound.MAXCOUNT];

    [SerializeField, Header("BGM AuidoSource")]
    AudioSource bgmSource;
    [SerializeField, Header("SFX AuidoSource")]
    AudioSource sfxSource;
    Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();


}
