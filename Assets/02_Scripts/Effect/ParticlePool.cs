using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePool : PoolableMono
{
    public override void Reset()
    {
        
    }

    private void OnParticleSystemStopped()
    {
        PoolManager.Instance.Push(this);
    }
}
