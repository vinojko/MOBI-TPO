using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using UnityEngine.SceneManagement;

public class CPRAED : MonoBehaviour
{
    // Start is called before the first frame update
    public static CPRAED instance;
    private int secElapsed;
    private int taps = 0;
    bool firstClick = true;

    private float respirationTimerStart;
    private float respirationTimerEnd;
    private float respirationTimerDiff = 0f;
    public bool released = false;


    public TextMeshProUGUI bpmText;
    public TextMeshProUGUI compressionCounterText;
    public TextMeshProUGUI respirationCounterText;

    [SerializeField]
    float ambulanceArrival = 30f;

    float bpm = 0f;
    float lastBpm = 0f;
    int neededSec = 0;
    int cprCounter = 0;

    private int respirationCounter = 0;

    float last, now, diff, sum, entries;


    public Slider mainSlider;
    public Slider depthSlider;
    public GameObject hands;
    public CanvasGroup fadeImage;
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
    float resultValue = 0.0f;

    [SerializeField]
    private int cycle = 0;

    public GameObject lungs;
    public Image lungsFilled;
    public Image lungsImg;

    Scene currentScene;

    bool doVibrate = true;


    public DialogTrigger respirationDilaog;
    public DialogTrigger chestCompression;
    public DialogTrigger lungHold;
    public DialogTrigger lungTooMuch;
    public DialogTrigger lungTooLittle;
    public DialogTrigger lungOkay;

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
        handButton.interactable = true;

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

        if (state == GameState.CPR)
        {
            UIAnimation();
            ChangeCameraCPR();
            StartCoroutine(AmbulanceArrival());
            FindObjectOfType<AudioManager>().Play("Ambulance");

            HandFadeIn();
            


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

        if (cprCounter == 30)
        {
            StartCoroutine(Respiration());
            cprCounter = 0;
            doVibrate = false;

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
        
        LeanTween.moveLocal(cprUI, new Vector3(0f, -785f, 0f), 3f).setEase(LeanTweenType.easeOutExpo).setDelay(1f);
     

    }
    private void UIAnimationClose()
    {
        LeanTween.moveLocal(cprUI, new Vector3(0f, -4000f, 0f), 3f).setEase(LeanTweenType.easeOutExpo);
        lungs.SetActive(false);
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
        //FaderMouth.instance.FadeDepth();

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

    void FadeImageIn()
    {
        LeanTween.reset();
        LeanTween.value(gameObject, 0f, 1f, 0.4f).setOnUpdate((value) =>
        {
            fadeImage.alpha = value;

        });
    }

    void FadeImageOut()
    {
        LeanTween.reset();
        LeanTween.value(gameObject, 1f, 0f, 0.4f).setOnUpdate((float value) =>
        {
            fadeImage.alpha = value;

        });
    }
    private void ShowLungs()
    {
        FadeImageIn();
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

  
    private IEnumerator MoveChin()
    {
        animator.SetBool("playReverseChin", false);
        animator.SetBool("playChin", true);
        ButtonSingleton.instance.leftShoulder = false;
        ButtonSingleton.instance.rightShoulder = false;


        chinLift.SetActive(false);
        yield return new WaitForSeconds(1.0f);
        respirationIcon.SetActive(true);
       
    }

    private IEnumerator AmbulanceArrival()
    {
        yield return new WaitForSeconds(ambulanceArrival);
        UIAnimationClose();
        hands.SetActive(false);
        respirationIcon.SetActive(false);
        chinLift.SetActive(false);
   
        GameManager.instance.UpdateGameState(GameState.AEDKoncano);


    }
    public void LungFillStart()
    {
        //0.634f - max
        //0.45f - min
        FindObjectOfType<AudioManager>().Play("BreathIn");
        LeanTween.value(gameObject, 0f, 1f, 2f).setOnUpdate((value) =>
        {
            if (released)
            {
                resultValue = value;

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
        if (resultValue > 0.634f)
        {
            Debug.Log("Prevec");
            lungTooMuch.TriggerDialog();
            FindObjectOfType<ButtonInteraction>().WrongAnswerSFX();
            yield return new WaitForSeconds(2.5f);

        }
        else if (resultValue < 0.45f)
        {
            Debug.Log("Premal");
            lungTooLittle.TriggerDialog();
            FindObjectOfType<ButtonInteraction>().WrongAnswerSFX();
            yield return new WaitForSeconds(2.5f);
        }
        else
        {
            Debug.Log("Okay");
            lungOkay.TriggerDialog();
            FindObjectOfType<ButtonInteraction>().CorrectAnswerSFX();
            yield return new WaitForSeconds(1f);
        }


        FadeImageOut();
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
      
            }
            else
            {
           
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

      

        }
    }



}



