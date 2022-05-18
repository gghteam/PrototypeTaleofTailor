using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoSingleton<UIManager>
{
    public bool isSetting = false;
    public Image steminaBar;
    public GameObject bar;

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

    public void UseSteminaFailedEffect()
    {
        bar.transform.DOShakePosition(2);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
