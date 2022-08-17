using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Call112 : MonoBehaviour
{

    public GameObject Answers;
    public Animator kiraAnim;
    public GameObject phone;

    public DialogTrigger callAmbulance, wrongNumber, correctNumber;

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
        else
        {
            
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

    public void checkNumber()
    {

        if (phoneText.text.Equals("112"))
        {
            correctNumber.TriggerDialog();
            PhoneAnimationEnd();

            StartCoroutine(EndCamera());
            
        }
        else
        {
            wrongNumber.TriggerDialog();
        }
    }

    private IEnumerator EndCamera()
    {
        yield return new WaitForSeconds(0.7f);
        ChangeCamera.instance.ChangeToCameraSlow(first);

        yield return new WaitForSeconds(0.5f);
        GameManager.instance.UpdateGameState(GameState.DihanjeKoncano);
    }

    private IEnumerator EndGame()
    {
        yield return new WaitForSeconds(1f);
        ChangeCamera.instance.ChangeToCameraSlow(first);
    }

}
