using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HeartIndicator : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Image heartRed;
    void Start()
    {
        StartCoroutine(showHeart());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator showHeart()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5455f);
            heartAnimationFadeIn();
            heartAnimationFadeOut();
        }
     
    }

    private void heartAnimationFadeIn()
    {
        LeanTween.value(gameObject, 0f, 1f, 0.2f).setOnUpdate((value) =>
        {
            var tempColor = heartRed.color;
            tempColor.a = value;
            heartRed.color = tempColor;
        });
    }
    private void heartAnimationFadeOut()
    {
        LeanTween.value(gameObject, 1f, 0f, 0.2f).setDelay(0.1f).setOnUpdate((value) =>
        {
            var tempColor = heartRed.color;
            tempColor.a = value;
            heartRed.color = tempColor;
        });
    }
}
