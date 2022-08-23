using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Depth : MonoBehaviour
{
    // Start is called before the first frame update
    public static Depth instance;
    float timer;

    public Slider depthSlider;

    public GameObject depthUI;
    void Start()
    {
        timer = 0f;
        instance = this;   
    }

    // Update is called once per frame
    void Update()
    {
        depthSlider.value = timer + Time.deltaTime;
        timer += Time.deltaTime;
    }

    public void ResetTimer()
    {
        timer = 0f;
    }

    public void DepthAnimation()
    {
        LeanTween.moveLocal(depthUI, new Vector3(-920f, 0f, 0f), 3f).setEase(LeanTweenType.easeOutExpo);
    }

    public void DepthAnimationClose()
    {
        LeanTween.moveLocal(depthUI, new Vector3(-5000f, 0f, 0f), 3f).setEase(LeanTweenType.easeOutExpo);
    }
}
