using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AEDPads : MonoBehaviour
{
    // Start is called before the first frame update

    public static AEDPads instance;
    public DialogTrigger analysisDialog;
    public bool leftPadSet, rightPadSet = false;

    public bool clicked = false;

    public GameObject light;
    public GameObject ghostHands;
    public Animator animator;
    public Camera AEDCam;
    public Camera defaultCam;

    public DialogTrigger shockClicked;

    public GameObject pads;
    public GameObject vsistranButton;

    public bool isClicked = false;
    void Awake()
    {
        instance = this;
    }

    public void PadsSet()
    {
        if(leftPadSet && rightPadSet)
        {
            analysisDialog.TriggerDialog();
            StartCoroutine(FlashLight());
            StartCoroutine(BlippingAEDSound());
            StartCoroutine(ChargingAEDSound());
            pads.transform.DOLocalMove(new Vector3(-0.418f, 3.29f, 2.839f), 1f);
            FadeIn();
            ghostHands.SetActive(false);
        }
        
    }

    private IEnumerator FlashLight()
    {
        yield return new WaitForSeconds(10f);
        ChangeCamera.instance.ChangeToCamera(AEDCam);

        GameManager.instance.UpdateGameState(GameState.AEDShock);

        while (!clicked)
        {
            light.SetActive(true);
            yield return new WaitForSeconds(0.20f);
            light.SetActive(false);
            yield return new WaitForSeconds(0.20f);
        }
        FindObjectOfType<AudioManager>().Stop("AEDBlipping");


        light.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        ChangeCamera.instance.ChangeToCamera(defaultCam);
        yield return new WaitForSeconds(2f);

        //Sound

        shockClicked.TriggerDialog();
        yield return new WaitForSeconds(2f);
        GameManager.instance.UpdateGameState(GameState.CPR);

    }

    private IEnumerator BlippingAEDSound()
    {
        yield return new WaitForSeconds(10f);
        FindObjectOfType<AudioManager>().Play("AEDBlipping");


    }
    private IEnumerator ChargingAEDSound()
    {
        yield return new WaitForSeconds(5f);
        FindObjectOfType<AudioManager>().Play("AEDCharging");


    }

    public void VsiStran()
    {
        isClicked = true;
        FadeOut();
    }

    private void FadeOut() 
    {
        LeanTween.moveLocal(vsistranButton, new Vector3(0f, -2035f, 0f), 1.5f).setEaseInOutExpo();

    }

    private void FadeIn()
    {
        LeanTween.moveLocal(vsistranButton, new Vector3(0f, -850f, 0f), 1.5f).setDelay(10.2f).setEaseInOutExpo();

    }

    private IEnumerator ShockClickedCoroutine()
    {
        FindObjectOfType<AudioManager>().Play("AEDClick");
        FindObjectOfType<AudioManager>().Play("AEDChargeShock");
        yield return new WaitForSeconds(2.5f);
        Vibration.Vibrate(20);
        StartCoroutine(HanzAnimation());

    }
    public void ShockClicked()
    {
        StartCoroutine(ShockClickedCoroutine());

    }

    public IEnumerator HanzAnimation()
    {
      
        animator.SetBool("playCPR", true);
        yield return new WaitForSeconds(0.2f);
        animator.SetBool("playCPR", false);

    }

}
