using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Lost : MonoBehaviour
{
    // Start is called before the first frame update
    public CanvasGroup endText, btn, results;
    public DataManager dataManager;
    public TextMeshProUGUI vp;
    void Start()
    {
        dataManager.Load();
        endText.alpha = 0f;
        btn.alpha = 0f;
        FindObjectOfType<AudioManager>().StopAll();
        StartCoroutine(end());
        vp.text = dataManager.data.verjetnostPrezivetja.ToString() + "%.";
        
    }

   
    IEnumerator end()
    {
        yield return new WaitForSeconds(2f);
        showText();
        yield return new WaitForSeconds(1f);
        showResults();
        yield return new WaitForSeconds(1.5f);
        showBtn();
    }

    private void showText()
    {
        LeanTween.value(gameObject, 0f, 1f, 1.3f).setOnUpdate((value) =>
        {
            endText.alpha = value;
        });
    }
    private void showResults()
    {
        LeanTween.value(gameObject, 0f, 1f, 1.3f).setOnUpdate((value) =>
        {
            results.alpha = value;
        });
    }

    private void showBtn()
    {
        LeanTween.value(gameObject, 0f, 1f, 1.3f).setOnUpdate((value) =>
        {
            btn.alpha = value;
        });
    }
}
