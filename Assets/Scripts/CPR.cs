using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using UnityEngine.SceneManagement;

public class CPR : MonoBehaviour
{
    // Start is called before the first frame update
    public static CPR instance;
    [SerializeField]
    private int cycle = 0;
    [SerializeField]
    private int neededCompressions = 30;
    private int secElapsed;
    private int taps = 0;
    public bool released = false;
    bool firstClick = true;

    private float respirationTimerStart;
    private float respirationTimerEnd;
    private float respirationTimerDiff = 0f;
    private float lungTimer = 0.0f;

    public TextMeshProUGUI bpmText;
    public TextMeshProUGUI compressionCounterText;
    public TextMeshProUGUI respirationCounterText;

    public DialogTrigger respirationDilaog;
    public DialogTrigger chestCompression;

    float bpm = 0f;
    float lastBpm = 0f;
    int neededSec = 0;
    int cprCounter = 0;

    private int respirationCounter = 0;

    float last, now, diff, sum, entries;


    public Slider mainSlider;
    public Slider depthSlider;
    public GameObject hands;
    public Animator animator;

    private Vector3 initHands;

    public GameObject cprUI, hand;
    public Button handButton;
    public GameObject respirationIcon;
    public GameObject chinLift;

    public Camera defaultCam, respirationCam, CPRCam, headCam, mouthCam;

    List<float> bpms = new List<float>();

    public Image compressionRing, respirationRing;

    float lerpSpeed;

   

    public GameObject lungs;
    public Image lungsFilled;
    public Image lungsImg;

    Scene currentScene;

    bool doVibrate = true;

    // Retrieve the name of this scene.
    string sceneName;
    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
        secElapsed = 0;
        taps = 0;
        initHands = new Vector3(3.03889f, 6.0774f, 3.798612f);
        respirationIcon.SetActive(false);
        handButton.interactable = false;

    }

    private void Update()
    {

        if (ButtonSingleton.instance.leftShoulder  && ButtonSingleton.instance.rightShoulder)
        {
            //StartCoroutine(RespirationClicked());
            StartCoroutine(MoveChin());


        }
        lerpSpeed = 5f * Time.deltaTime;

        compressionCounterText.text = cprCounter.ToString();
        respirationCounterText.text = respirationCounter.ToString();

        compressionRing.fillAmount = Mathf.Lerp(compressionRing.fillAmount, cprCounter / 30f, lerpSpeed);
        respirationRing.fillAmount = Mathf.Lerp(respirationRing.fillAmount, respirationCounter / 2f, lerpSpeed);
    }

    void Awake()
    {
        GameManager.OnGameStateChanged += GameManagerOnStateChanged;
        instance = this;
    }


    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManagerOnStateChanged;
    }

    private void GameManagerOnStateChanged(GameState state)
    {

        if (state == GameState.CPR /* || state == GameState.CPRAED*/)
        {
            StartCoroutine(UIAnimation());
            ChangeCameraCPR();
            StartCoroutine(Vibrate());

            if (sceneName == "5 - AED")
            {
                HandFadeIn();
            }


            //Depth.instance.DepthAnimation();
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
        Depth.instance.ResetTimer();
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

        if (cprCounter == neededCompressions)
        {
            respirationDilaog.TriggerDialog();
            StartCoroutine(Respiration());
            cprCounter = 0;
            doVibrate = false;
            StopCoroutine(Vibrate());
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

    private IEnumerator UIAnimation()
    {
        
        LeanTween.moveLocal(cprUI, new Vector3(0f, -785f, 0f), 3f).setEase(LeanTweenType.easeOutExpo).setDelay(8f);
        yield return new WaitForSeconds(8.0f);
        handButton.interactable = true;

    }
    private void UIAnimationClose()
    {
        LeanTween.moveLocal(cprUI, new Vector3(0f, -4000f, 0f), 3f).setEase(LeanTweenType.easeOutExpo);
        //Depth.instance.DepthAnimationClose();
    }

    IEnumerator Respiration()
    {
        respirationTimerStart = Time.time;
        hands.SetActive(false);
        //respirationIcon.SetActive(true);
        ChangeCamera.instance.ChangeToCamera(respirationCam);
        yield return new WaitForSeconds(1f);
        chinLift.SetActive(true);
        

    }

    public IEnumerator RespirationClicked()
    {

        respirationCounter++;

        respirationIcon.SetActive(false);
        //chinLift.SetActive(false);

        ChangeCamera.instance.ChangeToCamera(mouthCam);
        yield return new WaitForSeconds(0.4f);
        FaderMouth.instance.FadeDepth();

        lungs.SetActive(true);

        //Pljuca - 500 ml
        ShowLungs();


        
    }

    public void RespirationMouth()
    {
        
         StartCoroutine(RespirationClicked());
        
            
    }

    private void ChangeCameraCPR()
    {
        ChangeCamera.instance.ChangeToCamera(CPRCam);
    }

    private void FillLungs()
    {
        LeanTween.value(gameObject, 0f, 0.63f, 1.2f).setOnUpdate((value) =>
        {
            lungsFilled.fillAmount = value;
            
        });
    }

    private void ShowLungs()
    {
        StartCoroutine(showLungs());
    }

    IEnumerator showLungs()
    {
        lungs.SetActive(true);

        LeanTween.value(gameObject, 0f, 1f, 1f).setOnUpdate((value) =>
        {
            var tempColor = lungsFilled.color;
            tempColor.a = value;
            lungsFilled.color = tempColor;

            var tempColor2 = lungsImg.color;
            tempColor2.a = value;
            lungsImg.color = tempColor2;
        });

        yield return null;



    }

    private void HideLungs()
    {
        LeanTween.value(gameObject, 1f, 0f, 0.5f).setOnUpdate((value) =>
        {
            var tempColor = lungsFilled.color;
            tempColor.a = value;
            lungsFilled.color = tempColor;

            var tempColor2 = lungsImg.color;
            tempColor2.a = value;
            lungsImg.color = tempColor2;
        });

    }

    private void HandFadeIn()
    {
        LeanTween.moveLocal(hand, new Vector3(43f, -200f, 0f), 1f).setEase(LeanTweenType.easeInOutExpo);
    }

    private IEnumerator Vibrate()
    {
        FindObjectOfType<AudioManager>().Play("CPRRhytm");
        while (doVibrate)
        {
            yield return new WaitForSeconds(0.5455f);
            Vibration.Vibrate(20);
            Debug.Log("Vibrating 20ms");
        }

    }
    private IEnumerator MoveChin()
    {
        chinLift.SetActive(false);
        ButtonSingleton.instance.leftShoulder = false;
        ButtonSingleton.instance.rightShoulder = false;
        animator.SetBool("playReverseChin", false);
        animator.SetBool("playChin", true);
        


        
        yield return new WaitForSeconds(1.0f);
        respirationIcon.SetActive(true);
       
    }

    public void LungFillStart()
    {
        //0.634f - max
        //0.45f - min
        float resultValue;
        LeanTween.value(gameObject, 0f, 1f, 1.3f).setOnUpdate((value) =>
        {
            if (released)
            {
                if (value > 0.634f)
                {
                    Debug.Log("Prevec");
                }
                else if( value < 0.45f)
                {
                    Debug.Log("Premal");
                }
                else
                {
                    Debug.Log("Okay");
                }
                released = false;
                LeanTween.cancel(gameObject);
            };
            lungsFilled.fillAmount = value;
            resultValue = value;
            

        });

        Debug.Log("Lmao");


    }
    public void LungFillEnd()
    {
        released = true;
        StartCoroutine(LungFillEndCoroutine());

    }

    public IEnumerator LungFillEndCoroutine()
    {
    
        HideLungs();

        yield return new WaitForSeconds(.5f);
        lungs.SetActive(false);
        lungsFilled.fillAmount = 0f;

        ChangeCamera.instance.ChangeToCamera(respirationCam);

        if (GameManager.currentState != GameState.AEDKoncano)
        {
            ChangeCamera.instance.ChangeToCamera(respirationCam);
        }

        yield return new WaitForSeconds(0.5f);

        if (respirationCounter == 2)
        {
            //respirationIcon.SetActive(false);
            chinLift.SetActive(false);

            ChangeCamera.instance.ChangeToCamera(CPRCam);
            animator.SetBool("playReverseChin", true);
            yield return new WaitForSeconds(1.0f);
            hands.SetActive(true);
            respirationCounter = 0;
            animator.SetBool("playChin", false);

            chestCompression.TriggerDialog();





            respirationTimerEnd = Time.time;
            respirationTimerDiff = respirationTimerEnd - respirationTimerStart;
            last = respirationTimerEnd;

            cycle++;


            if (respirationCounter == 2 && cycle == 3)
            {
                doVibrate = false;
                StopCoroutine(Vibrate());
            }
            else
            {
                doVibrate = true;
                StartCoroutine(Vibrate());
            }
        }
        else
        {
            respirationIcon.SetActive(true);
            //chinLift.SetActive(true);
            hands.SetActive(false);
        }

        if (cycle == 3)
        {
            UIAnimationClose();
            hands.SetActive(false);
            respirationIcon.SetActive(false);
            chinLift.SetActive(false);

            if (sceneName == "4 - CPR")
            {
                GameManager.instance.UpdateGameState(GameState.CPRKira);
                FindObjectOfType<AudioManager>().Stop("CPRRhytm");

            }
            else if (sceneName == "5 - AED")
            {
                GameManager.instance.UpdateGameState(GameState.AEDKoncano);

            }

            doVibrate = false;
            StopCoroutine(Vibrate());

        }
    }



}
