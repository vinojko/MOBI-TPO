using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsideMouth : MonoBehaviour
{
    // Start is called before the first frame update


    public GameObject mouth;
    public DialogTrigger mouthDialog;
    public GameObject ui;


    private float AnimationSpeed = 0.5f;

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
        if (state == GameState.MouthInside)
        {
            StartCoroutine(StartDialog());
        }
    }


    public void AnswerYes()
    {
        StartCoroutine(Fade());
        GameManager.instance.UpdateGameState(GameState.CheckBreathing);
    }
    public void AnswerNo()
    {
        //Dialog napacno...
    }

   public IEnumerator Fade()
    {
        yield return new WaitForSeconds(0.5f);
        FaderMouth.instance.FadeIn();
        FaderMouth.instance.FadeOut();
        yield return new WaitForSeconds(0.5f);
        mouth.SetActive(false);
        

    }
    public IEnumerator StartDialog()
    {
        yield return new WaitForSeconds(1.5f);
        mouthDialog.TriggerDialog();
        yield return new WaitForSeconds(1.0f);
        ui.SetActive(true);



    }
}
