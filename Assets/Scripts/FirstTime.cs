using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstTime : MonoBehaviour
{
    // Start is called before the first frame update

    public DialogTrigger dialog1, dialog2;
    public GameObject hand;

    public GameObject firstButton, secondButton, thirdButton, fourthButton, fifthButton;

    public CanvasGroup canvas;

    public GameObject firstPanel;
    void Start()
    {
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
        yield return new WaitForSeconds(5f);
        FindObjectOfType<DialogFirstTime>().AnimationUIOpenUp();
    }

    public IEnumerator MoveHand()
    {
        yield return new WaitForSeconds(5f);

        LeanTween.moveLocal(hand, new Vector3(-16f, -277f, 0f), 3f).setEase(LeanTweenType.easeInOutExpo);
    }

    void ButtonsAnim()
    {
        LeanTween.scale(firstButton, new Vector3(1f, 1f, 1f), 1f).setDelay(0.4f).setEase(LeanTweenType.easeOutExpo);
        LeanTween.scale(secondButton, new Vector3(1f, 1f, 1f), 1f).setDelay(2.3f).setEase(LeanTweenType.easeOutExpo);
        LeanTween.scale(thirdButton, new Vector3(1f, 1f, 1f), 1f).setDelay(3.3f).setEase(LeanTweenType.easeOutExpo);
        LeanTween.scale(fourthButton, new Vector3(1f, 1f, 1f), 1f).setDelay(4.7f).setEase(LeanTweenType.easeOutExpo);
        LeanTween.scale(fifthButton, new Vector3(1f, 1f, 1f), 1f).setDelay(5.5f).setEase(LeanTweenType.easeOutExpo);
    }

    public IEnumerator Buttons()
    {
        yield return new WaitForSeconds(7f);
        dialog2.TriggerDialog();
        ButtonsAnim();
        yield return new WaitForSeconds(8.2f);
        FadeOut();
        yield return new WaitForSeconds(2.1f);
        firstPanel.SetActive(false);
    }

    private void FadeOut()
    {
        LeanTween.value(gameObject, 1f, 0f, 2f).setEase(LeanTweenType.easeInOutExpo).setOnUpdate((value) =>
        {
            canvas.alpha = value;
        });
    }
}
