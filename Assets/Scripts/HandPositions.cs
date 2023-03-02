using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPositions : MonoBehaviour
{
    [SerializeField] GameObject handPositions;
    public DialogTrigger startDialog, rightAnswerDialog, wrongAnswerDialog;

    //public DialogTrigger handPosDialog;
    void Awake()
    {
        GameManager.OnGameStateChanged += GameManagerOnStateChanged;
    }
    private void Start()
    {
        handPositionsAnim();
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManagerOnStateChanged;
    }

    private void GameManagerOnStateChanged(GameState state)
    {


    }

    private void handPositionsAnim()
    {

        //startDialog.TriggerDialog();
        LeanTween.moveLocal(handPositions, new Vector3(0f, 160f, 0f), 1.7f).setDelay(7.5f).setEase(LeanTweenType.easeOutExpo);
    }

    public void CloseHandPositions()
    {
        StartCoroutine(CloseHandPositionsCoroutine());
    }

    IEnumerator CloseHandPositionsCoroutine()
    {
        rightAnswerDialog.TriggerDialog();
        
        LeanTween.moveLocal(handPositions, new Vector3(0f, -760f, 0f), 1.7f).setDelay(0.3f).setEase(LeanTweenType.easeOutExpo);
        yield return new WaitForSeconds(2f);
        GameManager.instance.UpdateGameState(GameState.CPRPositions);
    }
    public void WrongAnswer()
    {
        wrongAnswerDialog.TriggerDialog();
        VPManager.instance.Decrease();
    }
}
