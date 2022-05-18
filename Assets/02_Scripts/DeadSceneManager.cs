using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeadSceneManager : MonoBehaviour
{
    [SerializeField]
    private Text deadText;

    private float timer;
   void Update()
    {
        timer += Time.deltaTime;
        if(timer >= 4)
        {
            SceneManager.LoadScene("Player_Scene");
        }
        if (deadText.color.a < 1)
        {
            deadText.color += new Color(0, 0, 0, Time.deltaTime * 0.5f);
        }
    }
}
