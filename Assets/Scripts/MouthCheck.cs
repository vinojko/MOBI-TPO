using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouthCheck : MonoBehaviour
{
    // Start is called before the first frame update
    public Image image;
    [SerializeField] private float AnimationSpeed = 0.4f;
    public Camera cam;

    private float delayTime = 1.45f;

    public GameObject mouthIcon;
    public GameObject mouth;

    public DialogTrigger mouthCheck, mouthWrong, mouthCorrect;



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
        if(state != GameState.MouthCheck)
        {
            mouthIcon.SetActive(false);
        }
        else
        {
            mouthIcon.SetActive(true);
            mouthCheck.TriggerDialog();
        }

    }
    void FadeIn()
    {

        LeanTween.value(gameObject, 0f, 1f, AnimationSpeed).setOnUpdate((value) =>
        {
            var tempColor = image.color;
            tempColor.a = value;
            image.color = tempColor;
        });

    }

    void MoveCamera()
    {
        ChangeCamera.instance.ChangeToCameraSlow(cam);
    }

    void FadeOut()
    {

        LeanTween.value(gameObject, 1f, 0f, AnimationSpeed).setDelay(delayTime).setOnUpdate((value) =>
        {
            var tempColor = image.color;
            tempColor.a = value;
            image.color = tempColor;
        });

    }

    public void Fade()
    {
        FadeIn();

        FadeOut();
    }

    public void HandleMouthButton()
    {
        GameManager.instance.UpdateGameState(GameState.MouthInside);
        MoveCamera();
        FadeIn();
        StartCoroutine(MouthEnable());
        FadeOut();

    }
    public IEnumerator MouthEnable()
    {
        yield return new WaitForSeconds(delayTime);
        mouth.SetActive(true);

        
    }

    public void MouthWrong()
    {
        mouthWrong.TriggerDialog();
        FindObjectOfType<AudioManager>().Play("Incorrect");
        VPManager.instance.Decrease();
    }
    public void MouthCorrect()
    {
        mouthCorrect.TriggerDialog();
        FindObjectOfType<AudioManager>().Play("Correct");
    }
}
