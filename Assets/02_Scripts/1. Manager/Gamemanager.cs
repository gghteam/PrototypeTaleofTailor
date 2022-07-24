using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemanager : MonoSingleton<Gamemanager>
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

    private static System.Random random = new System.Random();

    // Modern Fisher-Yates Shuffle
    public void Shuffle<T>(T[] array)
    {
        int n = array.Length;
        int last = n - 2;

        for (int i = 0; i <= last; i++)
        {
            int r = random.Next(i, n); // [i, n - 1]
            Swap(i, r);
        }

        // Local Method
        void Swap(int idxA, int idxB)
        {
            T temp = array[idxA];
            array[idxA] = array[idxB];
            array[idxB] = temp;
        }
    }
}
