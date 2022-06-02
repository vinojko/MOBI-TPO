using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsideMouth : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject brocolli;
    public GameObject mouth;

    private float AnimationSpeed = 0.5f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void moveBrocolli()
    {
        LeanTween.moveLocal(brocolli, new Vector3(1000f, 0f, 0f), 1.4f).setEase(LeanTweenType.easeOutExpo);
        StartCoroutine(Fade());
    }

   public IEnumerator Fade()
    {
        yield return new WaitForSeconds(0.5f);
        FaderMouth.instance.FadeIn();
        FaderMouth.instance.FadeOut();
        yield return new WaitForSeconds(0.5f);
        mouth.SetActive(false);
        

    }
}
