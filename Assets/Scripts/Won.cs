using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Won : MonoBehaviour
{

    // Start is called before the first frame update

    public Camera cam1, cam2, sky;
    public CanvasGroup canvas, credits;
    public Light fire;

    public GameObject clouds;
    void Start()
    {
        canvas.alpha = 0f;
        StartCoroutine(MoveCam());
        StartCoroutine(Fire());

        FindObjectOfType<AudioManager>().Play("Won");
    }

    private IEnumerator MoveCam()
    {
        yield return new WaitForSeconds(1f);
        ChangeCamera.instance.ChangeToCamera(cam1, 7f);
        yield return new WaitForSeconds(8.3f);
        
        showText();
        ChangeCamera.instance.ChangeToCamera(sky, 9f);
        yield return new WaitForSeconds(4f);
        showCredits();
    }

    private void showText()
    {
        LeanTween.value(gameObject, 0f, 1f, 1.3f).setOnUpdate((value) =>
        {
            canvas.alpha = value;
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
        clouds.transform.Translate(Vector3.forward * 0.1f * Time.deltaTime);
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
