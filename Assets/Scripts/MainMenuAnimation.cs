using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuAnimation : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject Hand, Logo, firstButton, secondButton, thirdButton, fourthButton, fifthButton;
    void Start()
    {
        //HandAnim();
        LogoAnim();
        //ButtonsAnim();  
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void HandAnim()
    {
        LeanTween.moveLocal(Hand, new Vector3(0f, -245f, 0f), 1.5f).setDelay(0.1f).setEase(LeanTweenType.easeOutExpo);
    }

    void LogoAnim()
    {
        RectTransform rectTransform = Logo.GetComponent<RectTransform>();
        LeanTween.move(rectTransform, new Vector2(150f, -300f), 1.5f).setDelay(0.1f).setEase(LeanTweenType.easeOutExpo);
    }

    void ButtonsAnim()
    {
        LeanTween.scale(firstButton, new Vector3(1f, 1f, 1f), 1f).setDelay(0.4f).setEase(LeanTweenType.easeOutExpo);
        LeanTween.scale(secondButton, new Vector3(1f, 1f, 1f), 1f).setDelay(0.6f).setEase(LeanTweenType.easeOutExpo);
        LeanTween.scale(thirdButton, new Vector3(1f, 1f, 1f), 1f).setDelay(0.8f).setEase(LeanTweenType.easeOutExpo);
        LeanTween.scale(fourthButton, new Vector3(1f, 1f, 1f), 1f).setDelay(1.0f).setEase(LeanTweenType.easeOutExpo);
        LeanTween.scale(fifthButton, new Vector3(1f, 1f, 1f), 1f).setDelay(1.2f).setEase(LeanTweenType.easeOutExpo);
    }
}
