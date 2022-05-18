using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField, Header("BGM ���� �����̴�")]
    private Slider bgmSlider;
    [SerializeField, Header("SFX ���� �����̴�")]
    private Slider sfxSlider;

    public bool isSetting = false;

    public void UiOpen(GameObject ui)
    {
        ui.SetActive(true);
    }
    public void UiClose(GameObject ui)
    {
        ui.SetActive(false);
    }

    public float BgmSoundBolume()
    {
        return bgmSlider.value;
    }

    public float SfxSoundBolume()
    {
        return sfxSlider.value;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
