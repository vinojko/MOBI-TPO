using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstTime : MonoBehaviour
{
    // Start is called before the first frame update

    public DataManager dataManager;
    public DialogTrigger dialog1, dialog2;
    public GameObject hand;

    public GameObject firstButton, secondButton, thirdButton, fourthButton, fifthButton;

    public CanvasGroup canvas;

    public GameObject firstPanel;
    void Start()
    {
        FindObjectOfType<AudioManager>().StopAll();
        FindObjectOfType<AudioManager>().Play("MainMenu");


        StartFirstTime();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartFirstTime()
    {
        dialog1.TriggerDialog();
        StartCoroutine(AnimationUp());
        StartCoroutine(MoveHand());
        StartCoroutine(Buttons());
    }

    public IEnumerator AnimationUp()
    {
        yield return new WaitForSeconds(6f);
        FindObjectOfType<DialogFirstTime>().AnimationUIOpenUp();
    }

    public IEnumerator MoveHand()
    {
        yield return new WaitForSeconds(5f);

        LeanTween.moveLocal(hand, new Vector3(-16f, -277f, 0f), 3f).setEase(LeanTweenType.easeInOutExpo);
    }

    void ButtonsAnim()
    {
        LeanTween.scale(firstButton, new Vector3(1f, 1f, 1f), 1f).setDelay(1.0f).setEase(LeanTweenType.easeOutExpo);
        LeanTween.scale(secondButton, new Vector3(1f, 1f, 1f), 1f).setDelay(2.5f).setEase(LeanTweenType.easeOutExpo);
        LeanTween.scale(thirdButton, new Vector3(1f, 1f, 1f), 1f).setDelay(4.8f).setEase(LeanTweenType.easeOutExpo);
        LeanTween.scale(fourthButton, new Vector3(1f, 1f, 1f), 1f).setDelay(9.0f).setEase(LeanTweenType.easeOutExpo);
        LeanTween.scale(fifthButton, new Vector3(1f, 1f, 1f), 1f).setDelay(12.0f).setEase(LeanTweenType.easeOutExpo);
    }

    public IEnumerator Buttons()
    {
        yield return new WaitForSeconds(8.5f);
        dialog2.TriggerDialog();
        ButtonsAnim();
        yield return new WaitForSeconds(14.5f);
        FadeOut();
        yield return new WaitForSeconds(4.1f);
        firstPanel.SetActive(false);
        dataManager.data.showTutorial = false;
        dataManager.Save();
    }

    private void FadeOut()
    {
        LeanTween.value(gameObject, 1f, 0f, 2f).setEase(LeanTweenType.easeInOutExpo).setOnUpdate((value) =>
        {
            canvas.alpha = value;
        });
    }
}
