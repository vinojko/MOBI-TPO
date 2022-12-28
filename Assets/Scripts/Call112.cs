using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Call112 : MonoBehaviour
{

    public GameObject Answers;
    public Animator kiraAnim;
    public GameObject phone, phone3D;

    public DialogTrigger callAmbulance, wrongNumber, correctNumber, instructions;

    public GameObject keypad, call;

    [SerializeField]TextMeshProUGUI phoneText;

    [SerializeField] Camera first;
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
            StartCoroutine(ShowAnswers());

        }
   

    }


    private IEnumerator ShowAnswers()
    {
        yield return new WaitForSeconds(1f);
        LeanTween.moveLocal(Answers, new Vector3(0f, -150f, 0f), 1.5f).setEaseInOutExpo();

    }

    public void AnswerRight()
    {
        LeanTween.moveLocal(Answers, new Vector3(0f, -1035f, 0f), 1.5f).setEaseInOutExpo();
        KiraPhoneAnimation();
        PhoneAnimation();
        callAmbulance.TriggerDialog();

    }

    private void KiraPhoneAnimation()
    {

        LeanTween.value(gameObject, 1f, 2f, 0.5f).setOnUpdate((value) =>
        {
            kiraAnim.SetFloat("Kira", value);
        });
    }

    private void PhoneAnimation()
    {
        LeanTween.moveLocal(phone, new Vector3(118f, -400f, 0f), 1.5f).setDelay(0.3f).setEaseInOutExpo();
    }
    private void PhoneAnimationEnd()
    {
        LeanTween.moveLocal(phone, new Vector3(118f, -2000f, 0f), 1.5f).setEaseInOutExpo();
    }

    public void CheckNumber()
    {

        if (phoneText.text.Equals("112"))
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

    private IEnumerator CallInstructions()
    {
        keypad.SetActive(false);
        call.SetActive(true);
        yield return new WaitForSeconds(1f);
        instructions.TriggerDialog();
        yield return new WaitForSeconds(10f);
        phone3D.SetActive(true);


        PhoneAnimationEnd();

        StartCoroutine(EndCamera());
    }


}
