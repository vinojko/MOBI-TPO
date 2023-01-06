using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IntroAnimationVarnost : MonoBehaviour
{
    // Start is called before the first frame update


    public GameObject number, title, hand, introScreen, holder;
    public CanvasGroup canvas, vsakaNapaka;

    private int probability = 100;
    public TextMeshProUGUI t;

    public GameObject gm, probabilityObject;



    void Start()
    {
       /* numberAnim();
        titleAnim();
        handAnim();
        AnimEnd();*/
    }
    void Awake()
    {
        GameManager.OnGameStateChanged += GameManagerOnStateChanged;
    }


    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManagerOnStateChanged;
    }

    private void GameManagerOnStateChanged(GameState state)
    {
        if(state == GameState.IntroAnimation)
        {
            StartCoroutine(Animate());
            FindObjectOfType<AudioManager>().Play("Theme");
        }

    }


    void numberAnim()
    {
        LeanTween.moveLocal(number, new Vector3(-2f, 695f, 0f), 2f).setDelay(0.5f).setEase(LeanTweenType.easeInOutExpo);
        LeanTween.scale(number, new Vector3(0.35f, 0.35f, 0.35f), 2f).setDelay(0.5f).setEase(LeanTweenType.easeInOutExpo);
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
        LeanTween.moveLocal(holder, new Vector3(0f, -4000f, 0f), 1.4f).setDelay(4.2f).setEase(LeanTweenType.easeInOutExpo);

    }

    void FadeIn()
    {
        LeanTween.value(gameObject, 0f, 1f, 1.2f).setDelay(1.6f).setOnUpdate((float value) =>
        {
            canvas.alpha = value;
            //Animate();
        });
    }

    private IEnumerator Animate()
    {
        yield return new WaitForSeconds(2f);
        introScreen.SetActive(true);
        FadeIn();
        yield return new WaitForSeconds(1.5f);
        numberAnim();
        titleAnim();
        handAnim();
        AnimEnd();

        //MOZNOST PREZIVETJA
        ProbFadeIn();
        yield return new WaitForSeconds(5.5f);
        //ChangeProb();
    }

    public void ChangeProb()
    {
        LeanTween.value(gameObject, 100, 90, 1.2f).setDelay(1.6f).setOnUpdate((float value) =>
        {
            t.text = ((int)value).ToString() + "%";
        });

    }

    void ProbFadeIn()
    {
        probabilityObject.SetActive(true);
        LeanTween.moveLocal(probabilityObject, new Vector3(-9f, 95f, 0f), 1.4f).setDelay(5f).setEase(LeanTweenType.easeInOutExpo);

        LeanTween.value(gameObject, 0f, 1f, 1.2f).setDelay(7.0f).setOnUpdate((float value) =>
        {
            vsakaNapaka.alpha = value;
        });

        LeanTween.moveLocal(introScreen, new Vector3(-9f, -5000f, 0f), 1.4f).setDelay(10f).setEase(LeanTweenType.easeInOutExpo).setOnComplete(() =>
        {
            GameManager.instance.UpdateGameState(GameState.Footsteps);
        });

 
    }



}
