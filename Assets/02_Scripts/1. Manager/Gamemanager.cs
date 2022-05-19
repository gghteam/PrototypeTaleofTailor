using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemanager : MonoBehaviour
{
	private void Awake()
	{
		DontDestroyOnLoad(this.gameObject);
	}
}
