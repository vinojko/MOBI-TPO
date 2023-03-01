using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AEDSign : MonoBehaviour
{
    [SerializeField]
    Camera AEDSignCamera;

    [SerializeField]
    DialogTrigger AEDSignDialog;
    [SerializeField]
    DialogTrigger correctAnswer, wrongAnswer;

    [SerializeField]
    GameObject yesNo;

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
        if (state == GameState.AEDSign)
        {
            StartCoroutine(AEDSignEnum());

        }
     

    }

    IEnumerator AEDSignEnum()
    {
        FaderMouth.instance.Fade();
        yield return new WaitForSeconds(0.5f);
        ChangeCamera.instance.ChangeToCamera(AEDSignCamera, 0.0f);
        yield return new WaitForSeconds(1f);
        AEDSignDialog.TriggerDialog();
        yield return new WaitForSeconds(1f);

        FadeIn();

        //Tule pridego gor odgovoti popa ce das drzi se aktivira 
    }

    public void CorrectAnswer()
    {
        StartCoroutine(CorrectAnswerCoroutine());

    }

    IEnumerator CorrectAnswerCoroutine()
    {
        correctAnswer.TriggerDialog();
        FadeOut();
        yield return new WaitForSeconds(1.1f);
        GameManager.instance.UpdateGameState(GameState.DihanjeKoncano);

    }

    public void WrongAnswer()
    {
        //FadeOut();
        wrongAnswer.TriggerDialog();
    }

    private void FadeIn()
    {
        LeanTween.moveLocal(yesNo, new Vector3(0f, 30f, 0f), 1.5f).setEaseInOutExpo();
    }
    private void FadeOut()
    {
        LeanTween.moveLocal(yesNo, new Vector3(0f, -2000f, 0f), 1.5f).setEaseInOutExpo();
    }
}

