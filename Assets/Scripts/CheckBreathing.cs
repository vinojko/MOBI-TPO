using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;


public class CheckBreathing : MonoBehaviour
{
    // Start is called before the first frame update
    public DialogTrigger checkBreathingDialog;
    public DialogTrigger timerDialog;
    public GameObject StopWatch;
    public GameObject CheckBreathingButton;
    public Camera TimerCam;

    public VolumeProfile mVolumeProfile;
    public Vignette mVignette;


    void Awake()
    {
        GameManager.OnGameStateChanged += GameManagerOnStateChanged;

    }

    private void Start()
    {
        CheckBreathingButton.SetActive(false);
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
        yield return new WaitForSeconds(2f);
        AnimateStopwatch();
        Timer.instance.ResetTimer();
    }

    private void AnimateStopwatch()
    {
        LeanTween.moveLocal(StopWatch, new Vector3(360f, 600f, 0f), 0.4f).setEaseInOutExpo();
    }

    public void toCamera()
    {
        ChangeCamera.instance.ChangeToCamera(TimerCam);
        CheckBreathingButton.SetActive(false);
        timerDialog.TriggerDialog();
        ChangeVignette();
        StartCoroutine(StartTimer());

    }

    private void ChangeVignette()
    {
        ClampedFloatParameter intensity = mVignette.intensity;
        LeanTween.value(gameObject, intensity.value, 0.5f, 0.5f).setOnUpdate((value) =>
        {
            intensity.value = value;
        });
    }
}
