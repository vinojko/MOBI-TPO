using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallHelp : MonoBehaviour
{
    public DialogTrigger dialog1;
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
    }
}
