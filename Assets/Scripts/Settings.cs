using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization.Settings;

public class Settings : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject settingsIcon;
    [SerializeField]
    GameObject settingsFrontLayer;
    [SerializeField]
    GameObject SLO, ENG, GER, MUTE;

    [SerializeField]
    Sprite muteIcon, unmutIcon;
    [SerializeField]
    Image muteButton;

    float languageAlpha = 0.5f;

    bool isMuted = false;

    public float animationSpeed;

    bool settingsStatus = true;

    public void SettingsOpen()
    {
        if (settingsStatus)
        {
            AnimationOpen();
            LeanTween.rotate(settingsIcon, new Vector3(0f, 0f, 90f), 0.3f).setEase(LeanTweenType.easeOutBack);

        }
        else
        {
            AnimationClose();
            LeanTween.rotate(settingsIcon, new Vector3(0f, 0f, 0f), 0.3f).setEase(LeanTweenType.easeOutBack);

        }

        settingsStatus = !settingsStatus;

    }

    public void AnimationOpen()
    {
        LeanTween.scale(SLO, new Vector3(1f, 1f, 1f), animationSpeed).setEase(LeanTweenType.easeOutExpo);
        LeanTween.scale(ENG, new Vector3(1f, 1f, 1f), animationSpeed).setEase(LeanTweenType.easeOutExpo).setDelay(0.1f);
        LeanTween.scale(GER, new Vector3(1f, 1f, 1f), animationSpeed).setEase(LeanTweenType.easeOutExpo).setDelay(0.2f);
        LeanTween.scale(MUTE,new Vector3(1f, 1f, 1f), animationSpeed).setEase(LeanTweenType.easeOutExpo).setDelay(0.3f);
    }
    public void AnimationClose()
    {
        LeanTween.scale(MUTE,new Vector3(0f, 0f, 0f), animationSpeed).setEase(LeanTweenType.easeOutExpo);
        LeanTween.scale(GER, new Vector3(0f, 0f, 0f), animationSpeed).setEase(LeanTweenType.easeOutExpo).setDelay(0.1f);
        LeanTween.scale(ENG, new Vector3(0f, 0f, 0f), animationSpeed).setEase(LeanTweenType.easeOutExpo).setDelay(0.2f);
        LeanTween.scale(SLO, new Vector3(0f, 0f, 0f), animationSpeed).setEase(LeanTweenType.easeOutExpo).setDelay(0.3f);

    }

    public void SLOSelected()
    {
        Image image = SLO.GetComponent<Image>();
        var tempColor = image.color;
        tempColor.a = 1f;
        image.color = tempColor;

        Image image2 = ENG.GetComponent<Image>();
        var tempColor2 = image2.color;
        tempColor2.a = languageAlpha;
        image2.color = tempColor2;

        Image image3 = GER.GetComponent<Image>();
        var tempColor3 = image3.color;
        tempColor3.a = languageAlpha;
        image3.color = tempColor3;

        StartCoroutine(SetLocale(0));
    }

    public void ENGSelected()
    {
        Image image = ENG.GetComponent<Image>();
        var tempColor = image.color;
        tempColor.a = 1f;
        image.color = tempColor;

        Image image2 = SLO.GetComponent<Image>();
        var tempColor2 = image2.color;
        tempColor2.a = languageAlpha;
        image2.color = tempColor2;

        Image image3 = GER.GetComponent<Image>();
        var tempColor3 = image3.color;
        tempColor3.a = languageAlpha;
        image3.color = tempColor3;

        StartCoroutine(SetLocale(1));
    }

    public void GERSelected()
    {
        Image image = GER.GetComponent<Image>();
        var tempColor = image.color;
        tempColor.a = 1f;
        image.color = tempColor;

        Image image2 = SLO.GetComponent<Image>();
        var tempColor2 = image2.color;
        tempColor2.a = languageAlpha;
        image2.color = tempColor2;

        Image image3 = ENG.GetComponent<Image>();
        var tempColor3 = image3.color;
        tempColor3.a = languageAlpha;
        image3.color = tempColor3;

        StartCoroutine(SetLocale(2));
    }

    public void Mute()
    {

        isMuted = !isMuted;
        if (isMuted)
        {
            muteButton.sprite = muteIcon;
            AudioListener.volume = 0;
        }
        else
        {
            muteButton.sprite = unmutIcon;
            AudioListener.volume = 1;
        }

        
    }

    IEnumerator SetLocale(int localeID)
    {
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localeID];
    }
}
