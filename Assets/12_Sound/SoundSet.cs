using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSet : MonoBehaviour
{
    [SerializeField]
    private AudioSource BGMaudioSource;

	private void Update()
    {
        BGMaudioSource.volume = UIManager.Instance.GetBgmVolume();
    }

    public void SrartBGM()
    {
        BGMaudioSource.Play();
    }
}
