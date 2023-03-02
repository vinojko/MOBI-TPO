using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AEDKoncan : MonoBehaviour
{
    public Camera houseCam;
    public DialogTrigger dialog;
    public GameObject ambulance, canvasFirst;
    public GameObject redLight, blueLight;
    public GameObject doctorIcon, AEDIcon;

    public Animator packageAnim;
    public Vector3 dragDistance;
    private Touch firstDetectedTouch;

    public Image fadeImage;
    public Image fadeImage2;
    void Awake()
    {
        GameManager.OnGameStateChanged += GameManagerOnStateChanged;
        ambulance.SetActive(false);
    }


    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManagerOnStateChanged;
    }

    private void GameManagerOnStateChanged(GameState state)
    {
        if (state == GameState.AEDKoncano /* || state == GameState.CPRAED*/)
        {
            ambulance.SetActive(true);
            StartCoroutine(Handle());

            doctorIcon.SetActive(true);
            AEDIcon.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        if (Input.touchCount > 0)
        {

            firstDetectedTouch = Input.GetTouch(0);

            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {

                dragDistance = firstDetectedTouch.deltaPosition;
                Debug.Log(dragDistance);
            }
        }
    }

    void Update()
    {
        RotateLights();
    }

    private IEnumerator Handle()
    {
        yield return new WaitForSeconds(0.5f);
        FaderMouth.instance.FadeIn();
        yield return new WaitForSeconds(1.3f);
        ChangeCamera.instance.ChangeToCamera(houseCam, 0.1f);
        yield return new WaitForSeconds(0.3f);
        FadeImageOut();
        canvasFirst.SetActive(false);
        

        yield return new WaitForSeconds(0.5f);
        dialog.TriggerDialog();
        yield return new WaitForSeconds(4.5f);
        FaderMouth.instance.FadeIn();
        yield return new WaitForSeconds(2f);
        WinOrLose();

    }

    private void WinOrLose()
    {
        if (VPManager.instance.vp >= 73)
        {
            SceneManager.LoadScene("Won");
        }
        else
        {
            SceneManager.LoadScene("Lost");
        }
    }
    private void RotateLights()
    {
        blueLight.transform.Rotate(new Vector3(10f, 0f, 0f));
        redLight.transform.Rotate(new Vector3(10f, 0f, 0f));
    }

    void FadeImageOut()
    {

        LeanTween.value(gameObject, 1f, 0f, 0.4f).setOnUpdate((value) =>
        {
            var tempColor = fadeImage.color;
            tempColor.a = value;
            fadeImage.color = tempColor;
        });


        LeanTween.value(gameObject, 1f, 0f, 0.4f).setOnUpdate((value) =>
        {
            var tempColor = fadeImage2.color;
            tempColor.a = value;
            fadeImage2.color = tempColor;
        });
    }
}
