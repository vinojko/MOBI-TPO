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
    public TextMeshProUGUI bpmText;

    float bpm = 0f;
    float lastBpm = 0f;
    int neededSec = 0;
    int cprCounter = 0;

    float last, now, diff, sum, entries;


    public Slider mainSlider;
    public GameObject hands;
    public Animator animator;

    private Vector3 initHands;

    public GameObject cprUI;
    public GameObject RespirationIcon;

    List<float> bpms = new List<float>();

    void Start()
    {
        secElapsed = 0;
        taps = 0;
        initHands = new Vector3 (3.03889f, 6.0774f, 3.798612f);
        
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

        if(state == GameState.CPR)
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

        if(taps > 2)
        {
            bpmText.text = bpmInt.ToString();
        }

        cprCounter++;

        if (cprCounter == 30) {
            Respiration();
            //cprCounter = 0;
        }

    }

    private void valueAnimation()
    {
        if(taps >= 3)
        {
            LeanTween.value(gameObject, lastBpm, bpm, 0.5f).setOnUpdate((value) =>
            {
                mainSlider.value = value;
            });
        }

    }

    private void HandsAnimation()
    {
        LeanTween.scale(hands, initHands - new Vector3(0.2f,0.2f,0.2f), 0.2f);
        LeanTween.scale(hands, initHands, 0.2f).setDelay(0.2f);
    }

   private void UIAnimation()
    {
        LeanTween.moveLocal(cprUI, new Vector3(0f, -810f, 0f), 3f).setEase(LeanTweenType.easeOutExpo);
    }

    private void Respiration()
    {
        hands.SetActive(false);
    }
}
