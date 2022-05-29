using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogoAnimation : MonoBehaviour
{

    public Image pulse;
    public float AnimationSpeed = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        pulse.fillAmount = 0;
        pulse.fillMethod = Image.FillMethod.Horizontal;
        pulse.fillOrigin = (int)Image.OriginHorizontal.Left;
        

    }

    // Update is called once per frame
    void Update()
    {

        if(pulse.fillAmount == 0)
        {
            ForwardPulse();
        }else if (pulse.fillAmount == 1)
        {

            BackwardPulse();
        }
        
    }

    void ForwardPulse()
    {
        pulse.fillAmount = 0;
        pulse.fillMethod = Image.FillMethod.Horizontal;
        pulse.fillOrigin = (int)Image.OriginHorizontal.Left;
        LeanTween.value(gameObject, 0f, 1f, AnimationSpeed).setOnUpdate( (value) =>
        {
            pulse.fillAmount = value;
        });
    }

    void BackwardPulse()
    {
        pulse.fillAmount = 1;
        pulse.fillMethod = Image.FillMethod.Horizontal;
        pulse.fillOrigin = (int)Image.OriginHorizontal.Right;
        LeanTween.value(gameObject, 1f, 0f, AnimationSpeed).setOnUpdate((value) =>
        {
            pulse.fillAmount = value;
        });
    }
}
