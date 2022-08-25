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
    public Camera AEDCam;
    public Camera defaultCam;

    public DialogTrigger shockClicked;

    public GameObject pads;
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
            pads.transform.DOLocalMove(new Vector3(-0.418f, 3.29f, 2.839f), 1f);
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
            yield return new WaitForSeconds(0.35f);
            light.SetActive(false);
            yield return new WaitForSeconds(0.35f);
        }

        light.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        ChangeCamera.instance.ChangeToCamera(defaultCam);
        yield return new WaitForSeconds(2f);

        //Sound

        shockClicked.TriggerDialog();
        
    }

}
