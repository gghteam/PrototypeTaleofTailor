using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startParticle : MonoBehaviour
{
	private static ParticleSystem particle;

	private void Awake()
	{
		particle = GetComponent<ParticleSystem>();
	}

	private void Start()
	{
		EventManager.StartListening("AttackParticle", Particle);
		particle.Stop();
	}

	private void Particle(EventParam eventParam)
	{
		particle.Play();
	}
}
