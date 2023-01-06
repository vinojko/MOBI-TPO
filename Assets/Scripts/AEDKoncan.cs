using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AEDKoncan : MonoBehaviour
{
    public Camera houseCam;
    public DialogTrigger dialog;
    public GameObject ambulance;
    public GameObject redLight, blueLight;
    public GameObject doctorIcon, AEDIcon;

    public Animator packageAnim;
    public Vector3 dragDistance;
    private Touch firstDetectedTouch;
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
        FaderMouth.instance.FadeDepth();
        yield return new WaitForSeconds(0.5f);
        ChangeCamera.instance.ChangeToCamera(houseCam , 0.1f);

        yield return new WaitForSeconds(0.5f);
        dialog.TriggerDialog();
        yield return new WaitForSeconds(7f);
        FaderMouth.instance.FadeDepth();
        yield return new WaitForSeconds(0.5f);
        WinOrLose();


    }

    private void WinOrLose()
    {
        if(VPManager.instance.vp >= 73)
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
}
