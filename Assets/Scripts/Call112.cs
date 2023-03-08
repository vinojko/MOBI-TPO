using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Call112 : MonoBehaviour
{

    public GameObject Answers;
    public Animator kiraAnim;
    public GameObject phone, phone3D;
    public Camera kiraCam;

    public DialogTrigger callAmbulance, wrongNumber, correctNumber, instructions, speakerDialog, wrongAnswer, rightAnswer;

    public GameObject keypad, call, speakerOn;

    [SerializeField]TextMeshProUGUI phoneText;

    [SerializeField] Camera first;
    public Animator kiraAnimator;

    int kiraHash;
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
        if (state == GameState.Call112)
        {
            

        }
   
    }
    void Start()
    {
        kiraHash = Animator.StringToHash("Kira");
        StartCoroutine(ShowAnswers());
    }

    private IEnumerator ShowAnswers()
    {
        yield return new WaitForSeconds(5f);

        ChangeCamera.instance.ChangeToCameraSlow(kiraCam);
        StartRunning();
        LeanTween.moveLocal(Answers, new Vector3(0f, -150f, 0f), 1.5f).setEaseInOutExpo();

    }

    public void AnswerRight()
    {
        LeanTween.moveLocal(Answers, new Vector3(0f, -1035f, 0f), 1.5f).setEaseInOutExpo();
        rightAnswer.TriggerDialog();
        StartCoroutine(AnswerRightCoroutine());

    }

    IEnumerator AnswerRightCoroutine()
    {

        yield return new WaitForSeconds(2f);
        LeanTween.moveLocal(Answers, new Vector3(0f, -1035f, 0f), 1.5f).setEaseInOutExpo();
        KiraPhoneAnimation();
        PhoneAnimation();
        callAmbulance.TriggerDialog();
        StartCoroutine(SpeakerOnDialog());

    }

    public void AnswerWrong()
    {
        wrongAnswer.TriggerDialog();

    }


    private void KiraPhoneAnimation()
    {

        LeanTween.value(gameObject, 1f, 2f, 0.5f).setOnUpdate((value) =>
        {
            kiraAnim.SetFloat("Kira", value);
        });
    }

    IEnumerator SpeakerOnDialog()
    {
        yield return new WaitForSeconds(16.7f);
        speakerDialog.TriggerDialog();
    }

    private void PhoneAnimation()
    {
        LeanTween.moveLocal(phone, new Vector3(118f, -400f, 0f), 1.5f).setDelay(0.3f).setEaseInOutExpo();
    }
    private void PhoneAnimationEnd()
    {
        LeanTween.moveLocal(phone, new Vector3(118f, -2000f, 0f), 1.5f).setEaseInOutExpo();
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
    public void CheckNumber()
    {

        if (phoneText.text.Equals("112") || phoneText.text.Equals("911"))
        {
            StartCoroutine(CallInstructions());
        }
        else
        {
            wrongNumber.TriggerDialog();
            VPManager.instance.Decrease();
        }
    }

    private IEnumerator EndCamera()
    {
        yield return new WaitForSeconds(0.7f);
        ChangeCamera.instance.ChangeToCameraSlow(first);

        yield return new WaitForSeconds(2.5f);
        GameManager.instance.UpdateGameState(GameState.SendForAED);
    }

    private IEnumerator EndGame()
    {
        yield return new WaitForSeconds(1f);
        ChangeCamera.instance.ChangeToCameraSlow(first);
    }

    public void SpeakerOn()
    {
        speakerOn.SetActive(true);
        PhoneAnimationEnd();
        StartCoroutine(EndCamera());

    }

    private IEnumerator CallInstructions()
    {
        keypad.SetActive(false);
        call.SetActive(true);
        yield return new WaitForSeconds(1f);
        instructions.TriggerDialog();
        yield return new WaitForSeconds(10f);
        phone3D.SetActive(true);


        

        
    }


}
