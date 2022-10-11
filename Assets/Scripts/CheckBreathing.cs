using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;


public class CheckBreathing : MonoBehaviour
{
    // Start is called before the first frame update
    public DialogTrigger checkBreathingDialog, timerDialog, timerEndDialog, wrongAnswer;
    public GameObject StopWatch;
    public GameObject CheckBreathingButton;
    public Camera TimerCam;
    public Camera chinCam;

    public VolumeProfile mVolumeProfile;
    public Vignette mVignette;
    public GameObject yesNo;
    private float VignetteSpeed = 1.5f;


    void Awake()
    {
        GameManager.OnGameStateChanged += GameManagerOnStateChanged;

    }

    private void Start()
    {
        //Za testiranje
        CheckBreathingButton.SetActive(false);
        yesNo.SetActive(false);
        for (int i = 0; i < mVolumeProfile.components.Count; i++)
        {
            if (mVolumeProfile.components[i].name == "Vignette")
            {
                mVignette = (Vignette)mVolumeProfile.components[i];
            }
        }
    }


    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManagerOnStateChanged;
    }

    private void GameManagerOnStateChanged(GameState state)
    {
        if (state == GameState.CheckBreathing)
        {
            StartCoroutine(Dialog1());
            StartCoroutine(EnableButton());
            //StartCoroutine(StartTimer());
        }


    }

    public IEnumerator Dialog1()
    {
        yield return new WaitForSeconds(1f);
        checkBreathingDialog.TriggerDialog();
    }

    public IEnumerator EnableButton()
    {
        yield return new WaitForSeconds(3.6f);
        CheckBreathingButton.SetActive(true);
    }

    public IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(1.0f);
        AnimateStopwatch();
        yield return new WaitForSeconds(1.5f);
        Timer.instance.ResetTimer();
        yield return new WaitForSeconds(4f);
        AgonalnoDihanje();
    }

    private void AnimateStopwatch()
    {
        LeanTween.moveLocal(StopWatch, new Vector3(360f, 600f, 0f), 1.5f).setEaseInOutExpo();
    }

    public void toCamera()
    {
        ChangeCamera.instance.ChangeToCameraSlow(TimerCam);
        CheckBreathingButton.SetActive(false);
        timerDialog.TriggerDialog();
        StartCoroutine(ChangeVignette());
        StartCoroutine(StartTimer());
        StartCoroutine(StopTimer());

    }



    public IEnumerator StopTimer()
    {
        yield return new WaitForSeconds(10f);
        LeanTween.moveLocal(StopWatch, new Vector3(787f, 600f, 0f), VignetteSpeed).setEaseInOutExpo();
        yield return new WaitForSeconds(1f);
        StopWatch.SetActive(false);
        DialogTimerEnd();
        VignetteRevert();
        RevertCamera();
        yield return new WaitForSeconds(0.5f);
        yesNo.SetActive(true);

    }

    private void DialogTimerEnd()
    {
        timerEndDialog.TriggerDialog();
    }
    public IEnumerator ChangeVignette()
    {
        yield return new WaitForSeconds(1f);
        ClampedFloatParameter intensity = mVignette.intensity;
        LeanTween.value(gameObject, intensity.value, 0.45f, 1.5f).setOnUpdate((value) =>
        {
            intensity.value = value;
        });
    }
    public void VignetteRevert()
    {
        ClampedFloatParameter intensity = mVignette.intensity;
        LeanTween.value(gameObject, intensity.value, 0.268f, VignetteSpeed).setOnUpdate((value) =>
        {
            intensity.value = value;
        });
    }

    public void RevertCamera()
    {
        ChangeCamera.instance.ChangeToCameraSlow(chinCam);
    }

    public void CorrectAnswer()
    {
        yesNo.SetActive(false);
        GameManager.instance.UpdateGameState(GameState.CallHelp);
    }

    public void WrongAnswer()
    {
        wrongAnswer.TriggerDialog();

        VPManager.instance.Decrease();
    }


    public void AgonalnoDihanje()
    {
        FindObjectOfType<AudioManager>().Play("AgonalnoDihanje");
    }
}

