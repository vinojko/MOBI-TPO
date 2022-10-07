using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPRPosition : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject cprPositions;
    public DialogTrigger dialog1, wrongAnswer, rightAnswer;
    void Awake()
    {
        GameManager.OnGameStateChanged += GameManagerOnStateChanged;
    }
    private void Start()
    {
        //handPositionsAnim();
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManagerOnStateChanged;
    }

    private void GameManagerOnStateChanged(GameState state)
    {
        if(state == GameState.CPRPositions)
        {
            dialog1.TriggerDialog();
            FadeIn();
        }

    }
    private void FadeIn()
    {

        //startDialog.TriggerDialog();
        LeanTween.moveLocal(cprPositions, new Vector3(200f, 160f, 0f), 1.7f).setDelay(1f).setEase(LeanTweenType.easeOutExpo);
    }

    public IEnumerator FadeOut()
    {
        //rightAnswerDialog.TriggerDialog();
        LeanTween.moveLocal(cprPositions, new Vector3(200f, -760f, 0f), 1.7f).setDelay(0.3f).setEase(LeanTweenType.easeOutExpo);
        yield return new WaitForSeconds(1f);
        GameManager.instance.UpdateGameState(GameState.Depth);
    }
    public void RightAnswer()
    {
        rightAnswer.TriggerDialog();
        StartCoroutine(FadeOut());
    }
    public void WrongAnswer()
    {
        wrongAnswer.TriggerDialog();
    }
}
