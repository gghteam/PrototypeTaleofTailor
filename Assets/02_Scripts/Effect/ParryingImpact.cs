using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryingImpact : PoolableMono
{
    private ParticleSystem particleSystem;

    private void Awake()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    public override void Reset()
    {
        
    }

    private void OnEnable()
    {
        particleSystem.Play();
    }

    public void OnParticleSystemStopped()
    {
        PoolManager.Instance.Push(this);
    }
}
