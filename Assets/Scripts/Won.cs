using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Won : MonoBehaviour
{

    // Start is called before the first frame update

    public Camera cam1, cam2, sky;
    public CanvasGroup canvas, credits;
    public GameObject nextButton, quiz;
    public Light fire;

    public GameObject clouds;
    void Start()
    {
        //canvas.alpha = 0f;
        StartCoroutine(MoveCam());
        StartCoroutine(Fire());
        FindObjectOfType<AudioManager>().StopAll();
        
        nextButton.SetActive(false);
        quiz.SetActive(false);
    }

    private IEnumerator MoveCam()
    {

        FindObjectOfType<AudioManager>().Play("Won");
        yield return new WaitForSeconds(1f);
        showText();
        yield return new WaitForSeconds(1f);
        ChangeCamera.instance.ChangeToCamera(sky, 9f);
        
        yield return new WaitForSeconds(2f);
        showCredits();
        yield return new WaitForSeconds(2f);
        ShowButton();
    }

    private void showText()
    {
        LeanTween.value(gameObject, 0f, 1f, 1.3f).setOnUpdate((value) =>
        {
            canvas.alpha = value;
        });
    }

    private void ShowButton()
    {
        nextButton.SetActive(true);
        LeanTween.value(gameObject, 0f, 1f, 1.3f).setOnUpdate((value) =>
        {
            nextButton.GetComponent<CanvasGroup>().alpha = value;
        });
    }
    public void ShowQuiz()
    {
        quiz.SetActive(true);
        LeanTween.value(gameObject, 0f, 1f, 2f).setOnUpdate((value) =>
        {
            quiz.GetComponent<CanvasGroup>().alpha = value;
        });
    }

    private void showCredits()
    {
        LeanTween.value(gameObject, 0f, 1f, 1.3f).setOnUpdate((value) =>
        {
            credits.alpha = value;
        });
    }

    private void Update()
    {
       // clouds.transform.Translate(Vector3.forward * 0.1f * Time.deltaTime);
    }

    private IEnumerator Fire()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.08f);
            fire.intensity = Random.Range(7f, 8.3f);
        }
      
    }
}
