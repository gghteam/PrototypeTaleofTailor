using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearManager : MonoBehaviour
{
    public void LoadGame()
    {
        SceneManager.LoadScene("Start");
    }

    public void GameEnd()
    {
        Application.Quit();
    }
}
