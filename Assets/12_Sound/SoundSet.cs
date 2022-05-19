using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSet : MonoBehaviour
{
    [SerializeField]
    private AudioSource BGMaudioSource;

	private void Start()
	{
        EventManager.StartListening("Start", StartBM);
	}
	private void Update()
    {
        BGMaudioSource.volume = UIManager.Instance.GetBgmVolume();
    }

    public void SrartBGM()
    {
        BGMaudioSource.Play();
    }

    public void StartBM(EventParam eventParam)
	{
        BGMaudioSource.Play();
    }
}
