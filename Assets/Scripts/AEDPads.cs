using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        }
        
    }

    private IEnumerator FlashLight()
    {
        yield return new WaitForSeconds(10f);
        ChangeCamera.instance.ChangeToCamera(AEDCam);

        while (!clicked)
        {
            light.SetActive(true);
            yield return new WaitForSeconds(0.35f);
            light.SetActive(false);
            yield return new WaitForSeconds(0.35f);
        }

        light.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        ChangeCamera.instance.ChangeToCamera(defaultCam);

        shockClicked.TriggerDialog();
        
    }

}
