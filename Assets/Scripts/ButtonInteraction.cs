using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CameraShake;

public class ButtonInteraction : MonoBehaviour
{
    ButtonInteraction instance;


    private void Awake()
    {
        instance = this;
    }
    public void CorrectAnswer(Button b)
            
    {
        ColorBlock cb = b.colors;
        cb.normalColor = Color.green;
        b.colors = cb;
    }

    public void WrongAnswer(Button b)

    {
        ColorBlock cb = b.colors;
        cb.normalColor = Color.green;
        b.colors = cb;

        CameraShaker.Presets.Explosion2D();
    }
}
