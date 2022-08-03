using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class Gamemanager : /*MonoSingleton<Gamemanager>*/MonoBehaviour
{
    public static Gamemanager Instance;

	[SerializeField] PoolingListSO poolList;

    #region SavePath&FileName
    private string savePath = "";
    private readonly string saveFileName = "PosSave";

    public string SavePath { get => savePath; }
    public string SaveFileName { get => saveFileName; }
    #endregion

    private void Awake()
	{
        if(Instance == null)
        {
            Instance = this;
        }
        else if(Instance != this)
        {
            Destroy(this);
        }

		//DontDestroyOnLoad(this.gameObject);

        savePath = Application.dataPath + "/Save";
        if (!Directory.Exists(savePath))
            Directory.CreateDirectory(savePath);

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

    #region Save&Load
    public void SaveJson<T>(string createPath, string fileName, T value)
    {
        FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", createPath, fileName), FileMode.Create);
        string json = JsonUtility.ToJson(value, true);
        byte[] data = Encoding.UTF8.GetBytes(json);
        fileStream.Write(data, 0, data.Length);
        fileStream.Close();
    }

    public T LoadJsonFile<T>(string loadPath, string fileName) where T : new()
    {
        if (File.Exists(string.Format("{0}/{1}.json", loadPath, fileName)))
        {
            FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", loadPath, fileName), FileMode.Open);
            byte[] data = new byte[fileStream.Length];
            fileStream.Read(data, 0, data.Length);
            fileStream.Close();
            string jsonData = Encoding.UTF8.GetString(data);
            return JsonUtility.FromJson<T>(jsonData);
        }
        SaveJson<T>(loadPath, fileName, new T());
        return LoadJsonFile<T>(loadPath, fileName);
    }
    #endregion
}
