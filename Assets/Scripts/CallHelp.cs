using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallHelp : MonoBehaviour
{
    public DialogTrigger dialog1, dialog2, dialog3;
    public GameObject Answers;
    public GameObject MicUI;
    public Camera kiraCam;

    private bool MicEnable = false;
    //Za tesatiranje mikrofona na racunalniku
    public bool micTesterPass = false;
    private float threshold = 0.30f;
    public Animator kiraAnimator;
    int kiraHash;
    void Awake()
    {
        GameManager.OnGameStateChanged += GameManagerOnStateChanged;

    }

    private void Start()
    {
        kiraHash = Animator.StringToHash("Kira");
    }


    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManagerOnStateChanged;
    }

    private void Update()
    {

        if ((MicEnable && MicInput.MicLoudness >= threshold) || micTesterPass == true)
        {
            MicEnable = false;
            dialog3.TriggerDialog();
            EndAnimation();
            ChangeCamera.instance.ChangeToCameraSlow(kiraCam);
            StartRunning();
            GameManager.instance.UpdateGameState(GameState.Call112);
        }
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
        LeanTween.moveLocal(MicUI, new Vector3(-300f, -850f, 0f), 2.2f).setDelay(1.7f).setEaseInOutExpo();
        MicEnable = true;
    }

    private void EndAnimation()
    {
        LeanTween.moveLocal(MicUI, new Vector3(-1200f, -850f, 0f), 1.7f).setDelay(0.2f).setEase(LeanTweenType.easeOutElastic);
    }

    private void StartRunning()
    {

        kiraAnimator.SetBool("StartRunning", true);
        /*LeanTween.value(gameObject, 1f, 2f, 0.5f).setOnUpdate((value) =>
        {
            kiraAnimator.SetFloat(kiraHash, value);
        });*/

        LeanTween.value(gameObject, 0f, 1f, 0.5f).setDelay(1.6f).setOnUpdate((value) =>
        {
            kiraAnimator.SetFloat(kiraHash, value);
        });
    }
}
