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
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        }
        else
        {
            wrongNumber.TriggerDialog();
        }
    }
}
