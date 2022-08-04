using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    Slider bgmSlider;
    [SerializeField]
    Slider sfxSlider;
    [SerializeField]
    Slider sensitivitySlider;
    public bool isSetting = false;
    public Image steminaBar;
    public GameObject bar;
    private bool isSound;
    public bool IsSound
    {
        get
        {
            return isSound;
        }
        set
        {
            isSound = value;
        }
    }

    private static UIManager instance = null;

    public static UIManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<UIManager>();
            }
            return instance;
        }
    }

    public void UiOpen(GameObject ui)
    {
        ui.SetActive(true);
    }
    public void UiClose(GameObject ui)
    {
        ui.SetActive(false);
    }
    
    public void SteminaBarValue()
    {
        steminaBar.fillAmount = SteminaManager.Instance.Stemina / SteminaManager.Instance.MAX_STEMINA;
    }

    public float GetBgmVolume()
    {
        return bgmSlider.value;
    }

    public float GetSfxVolume()
    {
        return sfxSlider.value;
    }

    public float GetsensitivityValue()
    {
        return sensitivitySlider.value;
    }
    public void UseSteminaFailedEffect()
    {
        bar.transform.DOShakePosition(2);
    }

    public void ChangeScene(string sc)
    {
        SceneManager.LoadScene(sc);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
