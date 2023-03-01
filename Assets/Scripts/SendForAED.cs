using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendForAED : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject yesNo;
    public DialogTrigger dialog;
    public DialogTrigger correctAnswerDialog;
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
        if (state == GameState.SendForAED)
        {
            FadeIn();
            dialog.TriggerDialog();

        }
        else
        {

        }

    }

    private void FadeIn()
    {
        LeanTween.moveLocal(yesNo, new Vector3(0f, 30f, 0f), 1.5f).setEaseInOutExpo();
    }
    private void FadeOut()
    {
        LeanTween.moveLocal(yesNo, new Vector3(0f, -2000f, 0f), 1.5f).setEaseInOutExpo();
    }
    public void CorrectAnswer()
    {
        LeanTween.moveLocal(yesNo, new Vector3(0f, 30f, 0f), 1.5f).setEaseInOutExpo();
        FadeOut();
        StartCoroutine(correctAnswer());

    }

    private IEnumerator correctAnswer()
    {
        correctAnswerDialog.TriggerDialog();
        yield return new WaitForSeconds(6f);
        GameManager.instance.UpdateGameState(GameState.AEDSign);
    }
}
