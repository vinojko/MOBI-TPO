using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HouseIntro : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject rotator, bus;
    public bool doRotate = true;

    public Camera startCam, windowCam;

    public GameObject buttons, rightAnswer, wrongAnswer;

    public Image panel;
    public CanvasGroup houseGroup;
    void Start()
    {
        ButtonsShow();
        StartCoroutine(RotateHouse());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Awake()
    {
        GameManager.OnGameStateChanged += GameManagerOnStateChanged;
    }


    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManagerOnStateChanged;
    }

    private void GameManagerOnStateChanged(GameState state)
    {
       

    }

    private IEnumerator RotateHouse()
    {
        
        while (doRotate)
        {
            rotator.transform.Rotate(0, -Time.deltaTime, 0);
            yield return new WaitForSeconds(0f);
        }
        
    }

    public void RightAnswer()
    {
        rightAnswer.SetActive(true);
        StartCoroutine(Answer());
    }
    public void WrongAnswer()
    {
        wrongAnswer.SetActive(true);
        StartCoroutine(Answer());
    }

    private IEnumerator Answer()
    {
        doRotate = false;
        LeanTween.moveLocal(buttons, new Vector3(0f, -677f, 0f), 3f).setEase(LeanTweenType.easeInOutExpo);
        FadeOut();

        yield return new WaitForSeconds(1.5f);

        ChangeCamera.instance.ChangeToCamera(windowCam, startCam);
        //yield return new WaitForSeconds(1.5f);
        //ChangeCamera.instance.ChangeToCameraSlow(startCam);
        GameManager.instance.UpdateGameState(GameState.Footsteps);
    }
    
    private void FadeOut()
    {
        LeanTween.value(gameObject, 1f, 0f, 3f).setEase(LeanTweenType.easeInOutExpo).setOnUpdate((value) =>
        {
            var tempColor = panel.color;
            tempColor.a = value;
            panel.color = tempColor;

            houseGroup.alpha = value;
        });
    }

    private void ButtonsShow()
    {
        LeanTween.moveLocal(buttons, new Vector3(0f, 0f, 0f), 3f).setEase(LeanTweenType.easeInOutExpo);

    }
}
