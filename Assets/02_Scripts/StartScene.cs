using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class StartScene : MonoBehaviour
{
    [Header("¾À ³Ñ¹ö")]
    [SerializeField]
    private int sceneNum;

    [SerializeField]
    private Text text;
	private void Start()
	{
        text.DOFade(0, .5f).SetLoops(-1, LoopType.Yoyo);
    }
	void Update()
    {
        if(Input.anyKey)
		{
            SceneManager.LoadScene(sceneNum);
		}
    }
}
