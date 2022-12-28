using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AEDKoncan : MonoBehaviour
{
    public Camera houseCam;
    public DialogTrigger dialog;
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

        if (state == GameState.AEDKoncano /* || state == GameState.CPRAED*/)
        {
            StartCoroutine(Handle());
        }
    }

    private IEnumerator Handle()
    {
        yield return new WaitForSeconds(0.5f);
        FaderMouth.instance.FadeDepth();
        yield return new WaitForSeconds(0.5f);
        ChangeCamera.instance.ChangeToCamera(houseCam , 0.1f);

        yield return new WaitForSeconds(0.5f);
        //dialog.TriggerDialog();
        yield return new WaitForSeconds(7f);
        FaderMouth.instance.FadeDepth();
        yield return new WaitForSeconds(0.5f);
        WinOrLose();


    }

    private void WinOrLose()
    {
        if(VPManager.instance.vp >= 72)
        {
            SceneManager.LoadScene("Won");
        }
        else
        {
            SceneManager.LoadScene("Lost");
        }
    }
}
