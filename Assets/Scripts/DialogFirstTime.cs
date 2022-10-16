using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogFirstTime : MonoBehaviour
{

    private Queue<string> sentences;
    public TextMeshProUGUI textMesh;

    public float TypeSpeed;
    private float defaultTypeSpeed;
    // Start is called before the first frame update
    private Color32 purple;
    char prevLetter = '+';

    public GameObject DialogUI;
    private bool first = true;
    void Start()
    {
        purple = new Color32(233, 3, 218, 255);
        sentences = new Queue<string>();
        defaultTypeSpeed = TypeSpeed;
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
        if (state == GameState.OdzivnostKoncano || state == GameState.VarnostKoncano || state == GameState.CPRKoncano || state == GameState.AEDKoncano)
        {
            AnimationUIClose();
        }


    }

    public void startDialog(Dialog dialog)
    {
        //animator.SetBool("isOpen", true);
        //Debug.Log("DIALOG TRIGGER");

        //LeanTween.moveLocal(DialogUI, new Vector3(0f, -35f, 0f), 1.7f).setDelay(0.2f).setEase(LeanTweenType.easeOutElastic);

        if (first) AnimationUIOpen();
        first = false;

        sentences.Clear();

        foreach (string sentence in dialog.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();


    }


    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialog();
            return;
        }


        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        textMesh.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            FindObjectOfType<AudioManager>().Play("Clack");

            //Purple #f700ce - ce bos rabu
            if (letter.Equals('['))
            {
                textMesh.text += "<color=#f700ce>";

            }
            textMesh.text += letter;

            if (prevLetter.Equals('.') && letter.Equals(' '))
            {
                TypeSpeed = 0.38f;
            }
            else if (letter.Equals('.'))
            {
                TypeSpeed = 0.16f;
            }
            else if (letter.Equals(','))
            {
                TypeSpeed = 0.16f;
            }
            else
            {
                TypeSpeed = defaultTypeSpeed;
            }

            yield return new WaitForSeconds(TypeSpeed);

            if (letter.Equals(']'))
            {
                textMesh.text += "</color>";
            }
            prevLetter = letter;

            //FindObjectOfType<AudioManager>().Stop("Type");
        }
    }
    void EndDialog()
    {
        AnimationUIClose();
    }

    void CheckCorrectState()
    {

    }

    void AnimationUIOpen()
    {
        LeanTween.moveLocalY(DialogUI, 80f, 2f).setDelay(0f).setEase(LeanTweenType.easeInOutQuart);
    }
    public void AnimationUIOpenUp()
    {
        LeanTween.moveLocalY(DialogUI, 800f, 2f).setDelay(0f).setEase(LeanTweenType.easeInOutQuart);
    }

    public void AnimationUIClose()
    {
        LeanTween.moveLocalY(DialogUI, 2085f, 0.9f).setDelay(0.2f).setEase(LeanTweenType.easeInOutQuart);
    }


}
