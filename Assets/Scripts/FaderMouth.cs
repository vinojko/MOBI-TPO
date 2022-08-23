using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FaderMouth : MonoBehaviour
{

    public static FaderMouth instance;
    [SerializeField]private  Image image;
    public float AnimationSpeed = 0.4f;
    private float delayTime = 1.45f;
    public void FadeIn()
    {

        LeanTween.value(gameObject, 0f, 1f, AnimationSpeed).setOnUpdate((value) =>
        {
            var tempColor = image.color;
            tempColor.a = value;
            image.color = tempColor;
        });

    }
    private void Awake()
    {
        instance = this;
    }


    public void FadeOut()
    {

        LeanTween.value(gameObject, 1f, 0f, AnimationSpeed).setDelay(delayTime).setOnUpdate((value) =>
        {
            var tempColor = image.color;
            tempColor.a = value;
            image.color = tempColor;
        });

    }

    public void FadeOutDepth()
    {

        LeanTween.value(gameObject, 1f, 0f, AnimationSpeed).setDelay(1f).setOnUpdate((value) =>
        {
            var tempColor = image.color;
            tempColor.a = value;
            image.color = tempColor;
        });

    }

    public void Fade()
    {
        FadeIn();
        FadeOut();
    }

    public void FadeDepth()
    {
        FadeIn();
        FadeOutDepth();
    }
}
