using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject settingsIcon;
    [SerializeField]
    GameObject settingsFrontLayer;
    [SerializeField]
    GameObject SLO, ENG, GER;

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
    }
    public void AnimationClose()
    {
        LeanTween.scale(GER, new Vector3(0f, 0f, 0f), animationSpeed).setEase(LeanTweenType.easeOutExpo);
        LeanTween.scale(ENG, new Vector3(0f, 0f, 0f), animationSpeed).setEase(LeanTweenType.easeOutExpo).setDelay(0.1f);
        LeanTween.scale(SLO, new Vector3(0f, 0f, 0f), animationSpeed).setEase(LeanTweenType.easeOutExpo).setDelay(0.2f);

    }

    public void SLOSelected()
    {
        LeanTween.scale(GER, new Vector3(0f, 0f, 0f), animationSpeed).setEase(LeanTweenType.easeOutExpo);
        LeanTween.scale(ENG, new Vector3(0f, 0f, 0f), animationSpeed).setEase(LeanTweenType.easeOutExpo).setDelay(0.1f);
        LeanTween.scale(SLO, new Vector3(0f, 0f, 0f), animationSpeed).setEase(LeanTweenType.easeOutExpo).setDelay(0.2f);
    }
}
