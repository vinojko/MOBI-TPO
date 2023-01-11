using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CameraShake;

public class ButtonInteraction : MonoBehaviour
{
    ButtonInteraction instance;
    Color32 green = new Color32(139, 255, 141, 255);
    Color32 red = new Color32(255, 143, 143, 255);
    Color32 white = new Color32(255, 255, 255, 255);


    private void Awake()
    {
        instance = this;
    }
    public void CorrectAnswer(Image image)        
    {
        Button button = image.gameObject.GetComponent<Button>();

        image.gameObject.GetComponent<Button>().interactable = false;
        image.color = green;

        ColorBlock colors = button.colors;
        Color disabledColor = colors.disabledColor;
        disabledColor = white;
        colors.disabledColor = disabledColor;
        button.colors = colors;
        FindObjectOfType<AudioManager>().Play("Correct");
    }

    public void CorrectAnswerSFX()
    {
        FindObjectOfType<AudioManager>().Play("Correct");
    }

    public void WrongAnswerSFX()
    {
        FindObjectOfType<AudioManager>().Play("Incorrect");
    }

    public void WrongAnswer(Image image)

    {
        Button button = image.gameObject.GetComponent<Button>();

        image.gameObject.GetComponent<Button>().interactable = false;

        ColorBlock colors = button.colors;
        Color disabledColor = colors.disabledColor;
        disabledColor = white;
        colors.disabledColor = disabledColor;
        button.colors = colors;
        image.color = red;
        CameraShaker.Presets.Explosion2D();
        FindObjectOfType<AudioManager>().Play("Incorrect");
    }
}
