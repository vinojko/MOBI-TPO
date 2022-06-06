using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallHelp : MonoBehaviour
{
    public DialogTrigger dialog1, dialog2;
    public GameObject Answers;
    public GameObject MicUI;
    void Awake()
    {
        GameManager.OnGameStateChanged += GameManagerOnStateChanged;

    }

    private void Start()
    {

    }


    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManagerOnStateChanged;
    }

    private void GameManagerOnStateChanged(GameState state)
    {
        if (state == GameState.CallHelp)
        {
            dialog1.TriggerDialog();
            StartCoroutine(ShowAnswers());
        }
    }

    private IEnumerator ShowAnswers()
    {
        yield return new WaitForSeconds(1f);
        LeanTween.moveLocal(Answers, new Vector3(0f, -50f, 0f), 1.5f).setEaseInOutExpo();

    }

    public void AnswerRight()
    {
        LeanTween.moveLocal(Answers, new Vector3(0f, -1035f, 0f), 1.5f).setEaseInOutExpo();
        StartAnimation();
        dialog2.TriggerDialog();

    }
    private void StartAnimation()
    {
        LeanTween.moveLocal(MicUI, new Vector3(-300f, -850f, 0f), 2.2f).setEaseInOutExpo();
    }

    private void EndAnimation()
    {
        LeanTween.moveLocal(MicUI, new Vector3(-1200f, -850f, 0f), 1.7f).setDelay(0.2f).setEase(LeanTweenType.easeOutElastic);
    }
}
