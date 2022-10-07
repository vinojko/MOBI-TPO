using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LinePositions : MonoBehaviour
{
    public Image line1, line2, line3, line4;
    public GameObject lines;
    public DialogTrigger lineDialog;
    public GameObject hand;
    public GameObject CPRSlider;
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
        lines.SetActive(state == GameState.LinePositions);

        if (state == GameState.LinePositions) {

            LineAnimations();
        } 
    
    }

    public void LineAnimations()
    {
        LeanTween.value(gameObject, 0f, 1f, 1.1f).setEase(LeanTweenType.easeInOutExpo).setOnUpdate((value) =>
        {
            line1.fillAmount = value;
        });

        LeanTween.value(gameObject, 0f, 1f, 1.1f).setDelay(0.5f).setEase(LeanTweenType.easeInOutExpo).setOnUpdate((value) =>
        {
            line2.fillAmount = value;
        });

        LeanTween.value(gameObject, 0f, 1f, 1.5f).setDelay(1f).setEase(LeanTweenType.easeInOutExpo).setOnUpdate((value) =>
        {
            line3.fillAmount = value;
        });

        LeanTween.value(gameObject, 0f, 1f, 1.5f).setDelay(1.5f).setEase(LeanTweenType.easeInOutExpo).setOnUpdate((value) =>
        {
            line4.fillAmount = value;
        });

        HandAnimation();
    }

    private void HandAnimation()
    {
        LeanTween.moveLocal(hand, new Vector3(43f, -200f, 0f), 1f).setDelay(2.5f).setEase(LeanTweenType.easeInOutExpo);
        StartCoroutine(ChangeState());
    }

    private IEnumerator ChangeState()
    {
        yield return new WaitForSeconds(4.0f);
        GameManager.instance.UpdateGameState(GameState.CPR);
    }


}
