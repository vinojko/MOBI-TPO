using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lost : MonoBehaviour
{
    // Start is called before the first frame update
    public CanvasGroup endText, btn;
    void Start()
    {
        endText.alpha = 0f;
        btn.alpha = 0f;
        StartCoroutine(end());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator end()
    {
        yield return new WaitForSeconds(2f);
        showText();
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

    private void showBtn()
    {
        LeanTween.value(gameObject, 0f, 1f, 1.3f).setOnUpdate((value) =>
        {
            btn.alpha = value;
        });
    }
}
