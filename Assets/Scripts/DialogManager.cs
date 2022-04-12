using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{

    private Queue<string> sentences;
    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

  public void startDialog(Dialog dialog)
    {
        Debug.Log("DIALOG TRIGGER");

        sentences.Clear();

        foreach (string sentence in sentences)
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

        sentences.Dequeue();
    }
    void EndDialog()
    {
        Debug.Log("End of dialog");
    }
}
