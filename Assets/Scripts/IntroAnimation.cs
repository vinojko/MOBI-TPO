using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroAnimation : MonoBehaviour
{
    // Start is called before the first frame update


    public GameObject number, title, hand, introScreen;
    void Start()
    {
        FindObjectOfType<AudioManager>().Play("Kitchen");
        numberAnim();
        titleAnim();
        handAnim();
        AnimEnd();
    }

    void numberAnim()
    {
        LeanTween.moveLocal(number, new Vector3(-2f, 695f, 0f), 2f).setDelay(0.5f).setEase(LeanTweenType.easeInOutExpo);
        LeanTween.scale(number, new Vector3(0.5f,0.5f,0.5f), 2f).setDelay(0.5f).setEase(LeanTweenType.easeInOutExpo);
    }

    void titleAnim()
    {
        LeanTween.moveLocal(title, new Vector3(0f, 120f, 0f), 1.4f).setDelay(1.7f).setEase(LeanTweenType.easeOutExpo);
        LeanTween.scale(title, new Vector3(1f, 1f, 1f), 0.5f).setDelay(1.0f).setEase(LeanTweenType.easeInOutExpo);
    }

    void handAnim()
    {
        LeanTween.moveLocal(hand, new Vector3(0f, -140f, 0f), 1.4f).setDelay(2f).setEase(LeanTweenType.easeInOutExpo);
    }

    void AnimEnd()
    {
        LeanTween.moveLocal(introScreen, new Vector3(0f, -4000f, 0f), 1.4f).setDelay(4.2f).setEase(LeanTweenType.easeInOutExpo);

    }


}
