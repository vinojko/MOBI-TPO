using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OsebaOdzivna : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject yesno;
    public DialogTrigger odzivna, wrongAnswer, rightAnswer, hrbetDialog;
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
        if (state == GameState.OsebaOdzivna)
        {
            odzivna.TriggerDialog();
            FadeIn();

        }
    }

    public void RightAnswer()
    {
        rightAnswer.TriggerDialog();
        GameManager.instance.UpdateGameState(GameState.PremakniZrtev);
        FadeOut();
        StartCoroutine(DialogTrig());
    }
    public void WrongAnswer()
    {
        wrongAnswer.TriggerDialog();

    }

    void FadeIn()
    {
        LeanTween.moveLocal(yesno, new Vector3(0f, -103f, 0f), 1.5f).setEaseInOutExpo();
    }

    void FadeOut()
    {
        LeanTween.moveLocal(yesno, new Vector3(0f, -2003f, 0f), 1.5f).setDelay(0.5f).setEaseInOutExpo();
    }

    IEnumerator DialogTrig()
    {
        yield return new WaitForSeconds(1f);
        hrbetDialog.TriggerDialog();

    }
}
