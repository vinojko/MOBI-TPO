using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class CPR : MonoBehaviour
{
    // Start is called before the first frame update
    private int secElapsed;
    private int taps = 0;
    bool firstClick = true;

    private float respirationTimerStart;
    private float respirationTimerEnd;
    private float respirationTimerDiff = 0f;

    public TextMeshProUGUI bpmText;
    public TextMeshProUGUI compressionCounterText;
    public TextMeshProUGUI respirationCounterText;



    float bpm = 0f;
    float lastBpm = 0f;
    int neededSec = 0;
    int cprCounter = 0;

    private int respirationCounter = 0;

    float last, now, diff, sum, entries;


    public Slider mainSlider;
    public GameObject hands;
    public Animator animator;

    private Vector3 initHands;

    public GameObject cprUI;
    public GameObject respirationIcon;

    public Camera defaultCam, respirationCam;

    List<float> bpms = new List<float>();

    public Image compressionRing, respirationRing;

    float lerpSpeed;

    private int cycle = 2;

    void Start()
    {
        secElapsed = 0;
        taps = 0;
        initHands = new Vector3(3.03889f, 6.0774f, 3.798612f);
        respirationIcon.SetActive(true);

    }

    private void Update()
    {
        lerpSpeed = 5f * Time.deltaTime;

        compressionCounterText.text = cprCounter.ToString();
        respirationCounterText.text = respirationCounter.ToString();

        compressionRing.fillAmount = Mathf.Lerp(compressionRing.fillAmount, cprCounter / 30f, lerpSpeed);
        respirationRing.fillAmount = Mathf.Lerp(respirationRing.fillAmount, respirationCounter / 2f, lerpSpeed);
    }

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

        if (state == GameState.CPR)
        {
            UIAnimation();
        }
    }

    public IEnumerator HanzAnimation()
    {

        animator.SetBool("playCPR", true);
        yield return new WaitForSeconds(0.2f);
        animator.SetBool("playCPR", false);




    }

    public void Tap()
    {
        lastBpm = bpm;

        if (firstClick)
        {
            last = Time.time;
            firstClick = false;
        }

        now = Time.time;
        diff = now - last;
        last = now;
        sum = sum + diff;

        bpms.Add(diff);

        entries++;

        float avg = bpms.Average();

        Debug.Log(60f / avg);
        bpm = 60f / avg;

        taps++;

        valueAnimation();
        HandsAnimation();
        StartCoroutine(HanzAnimation());

        int bpmInt = (int)bpm;

        if (taps > 2)
        {
            bpmText.text = bpmInt.ToString();
        }

        cprCounter++;

        if (cprCounter == 30)
        {
            Respiration();
            cprCounter = 0;
        }

        

    }

    private void valueAnimation()
    {
        if (taps >= 3)
        {
            LeanTween.value(gameObject, lastBpm, bpm, 0.5f).setOnUpdate((value) =>
            {
                mainSlider.value = value;
            });
        }

    }

    private void HandsAnimation()
    {
        LeanTween.scale(hands, initHands - new Vector3(0.2f, 0.2f, 0.2f), 0.2f);
        LeanTween.scale(hands, initHands, 0.2f).setDelay(0.2f);
    }

    private void UIAnimation()
    {
        LeanTween.moveLocal(cprUI, new Vector3(0f, -810f, 0f), 3f).setEase(LeanTweenType.easeOutExpo);
    }
    private void UIAnimationClose()
    {
        LeanTween.moveLocal(cprUI, new Vector3(0f, -4000f, 0f), 3f).setEase(LeanTweenType.easeOutExpo);
    }

    private void Respiration()
    {
        respirationTimerStart = Time.time;
        hands.SetActive(false);
        respirationIcon.SetActive(true);
    }

    public IEnumerator RespirationClicked()
    {

        respirationCounter++;

        respirationIcon.SetActive(false);
        ChangeCamera.instance.ChangeToCamera(respirationCam);
        yield return new WaitForSeconds(1.5f);
        ChangeCamera.instance.ChangeToCamera(defaultCam);
        yield return new WaitForSeconds(0.5f);

        if (respirationCounter == 2)
        {
            
            

            respirationIcon.SetActive(false);
            hands.SetActive(true);
            respirationCounter = 0;
            respirationCounter = 0;

            respirationTimerEnd = Time.time;
            respirationTimerDiff = respirationTimerEnd - respirationTimerStart;
            last = respirationTimerEnd;

            cycle++;
        }
        else
        {
            respirationIcon.SetActive(true);
            hands.SetActive(false);
        }

        if(cycle == 3)
        {
            UIAnimationClose();
            hands.SetActive(false);
            respirationIcon.SetActive(false);

            GameManager.instance.UpdateGameState(GameState.CPRKira);
            
        }
    }

    public void RespirationMouth()
    {
        StartCoroutine(RespirationClicked());
    }

}
