using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXSet : MonoBehaviour
{
    private static AudioSource VFXaudioSource;


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
