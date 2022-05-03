using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogManager : MonoBehaviour
{

    private Queue<string> sentences;
    public TextMeshProUGUI textMesh;

    public float TypeSpeed;
    // Start is called before the first frame update
    private Color32 purple;

    public GameObject DialogUI;
    void Start()
    {
        purple = new Color32(233, 3, 218, 255);
        sentences = new Queue<string>();
    }

  public void startDialog(Dialog dialog)
    {
        //animator.SetBool("isOpen", true);
        //Debug.Log("DIALOG TRIGGER");

        //LeanTween.moveLocal(DialogUI, new Vector3(0f, -35f, 0f), 1.7f).setDelay(0.2f).setEase(LeanTweenType.easeOutElastic);

        AnimationUIOpen();
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

    IEnumerator TypeSentence (string sentence)
    {
        textMesh.text = "";

        foreach (char letter in sentence.ToCharArray())
        {

            //Purple #f700ce - ce bos rabu
            if (letter.Equals('[')) {
                textMesh.text += "<color=#f700ce>";
            
            }
            textMesh.text += letter;
            yield return new WaitForSeconds(TypeSpeed);

            if (letter.Equals(']'))
            {
                textMesh.text += "</color>";
            }
        }
    }
    void EndDialog()
    {
        //animator.SetBool("isOpen", false);
    }

    void AnimationUIOpen()
    {
        LeanTween.moveLocalY(DialogUI, 1085f, 0.9f).setDelay(0.2f).setEase(LeanTweenType.easeInOutQuart);
    }
}
