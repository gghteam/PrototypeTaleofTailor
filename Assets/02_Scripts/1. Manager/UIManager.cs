using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoSingleton<UIManager>
{
    public bool isSetting = false;

    public void UiOpen(GameObject ui)
    {
        ui.SetActive(true);
    }
    public void UiClose(GameObject ui)
    {
        ui.SetActive(false);
    }
    
    public void Quit()
    {
        Application.Quit();
    }
}
