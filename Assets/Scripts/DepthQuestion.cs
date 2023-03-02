using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepthQuestion : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject answers;
    public DialogTrigger dialog1, rightAnswer, wrongAnswer;
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
        if (state == GameState.Depth)
        {
            dialog1.TriggerDialog();
            StartCoroutine(FadeIn());
        }

    }

    private IEnumerator FadeIn()
    {
        yield return new WaitForSeconds(1f);
        LeanTween.moveLocal(answers, new Vector3(0f, -50f, 0f), 1.5f).setEaseInOutExpo();

    }

    public void AnswerRight()
    {
        LeanTween.moveLocal(answers, new Vector3(0f, -1035f, 0f), 1.5f).setEaseInOutExpo();
        //StartAnimation();
        rightAnswer.TriggerDialog();
        StartCoroutine(ChangeState());

    }

    public void AnswerWrong()
    {
        wrongAnswer.TriggerDialog();
        VPManager.instance.Decrease();

    }

    private IEnumerator ChangeState()
    {
        yield return new WaitForSeconds(3.3f);
        GameManager.instance.UpdateGameState(GameState.BPM);
    }
}
