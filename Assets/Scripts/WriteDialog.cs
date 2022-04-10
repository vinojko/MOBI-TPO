using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WriteDialog : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI Dialog;
    private int Index = 0;
    public float DialogSpeed;

    public void NextSentence(string Sentence)
    {
   
        StartCoroutine(Write(Sentence));
        
    }
    IEnumerator Write(string Sentence)
    {

        foreach (char Character in Sentence.ToCharArray())
        {
            Dialog.text += Character;
            yield return new WaitForSeconds(DialogSpeed);
        }
        Index++;
        
     
    }
}
