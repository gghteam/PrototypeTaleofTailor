using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemanager : MonoBehaviour
{
	[SerializeField] PoolingListSO poolList;

	private void Awake()
	{
		DontDestroyOnLoad(this.gameObject);
        PoolManager.Instance = new PoolManager(transform);

		CreatePool();
	}

	void CreatePool()
    {
		foreach(PoolingPair pair in poolList.list)
        {
			PoolManager.Instance.CreatePool(pair.prefab, pair.poolCnt);
        }
    }
}
