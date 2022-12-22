using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AEDOn : MonoBehaviour
{
    // Start is called before the first frame update

    public string destinationTag = "AEDOn";
    public Material AEDOnMaterial;

    public Camera AEDCam;
    public Camera defaultCam;

    public GameObject doctorIcon, AEDIcon;

    public DialogTrigger dialog;

    public GameObject pads;
    void Start()
    {
        StartCoroutine(MoveCamera());
        doctorIcon.SetActive(true);
        AEDIcon.SetActive(false);
        GameManager.instance.UpdateGameState(GameState.AED);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        Debug.Log("Down");
    }

    void OnMouseUp()
    {
        if (GameManager.currentState == GameState.AED)
        {
            
            var rayOrigin = Camera.main.transform.position;
            var rayDirection = MouseWorldPosition() - Camera.main.transform.position;
            RaycastHit hitInfo;
            if (Physics.Raycast(rayOrigin, rayDirection, out hitInfo))
            {
                if (hitInfo.transform.CompareTag(destinationTag))
                {
                    Debug.Log("aed ON");
                    FindObjectOfType<AudioManager>().Play("AEDClick");
                    AEDOnMaterial.EnableKeyword("_EMISSION");
                    //ChangeCamera.instance.ChangeToCamera(defaultCam);
                    StartCoroutine(MoveCameraDefault());
                    StartCoroutine(Pads());

                }

            }

            transform.GetComponent<Collider>().enabled = true;
        }
    }
    
        

    Vector3 MouseWorldPosition()
    {
        var mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mouseScreenPos);
    }

    private IEnumerator MoveCamera()
    {

        yield return new WaitForSeconds(5f);
        ChangeCamera.instance.ChangeToCamera(AEDCam);
    }

    private IEnumerator MoveCameraDefault()
    {

        yield return new WaitForSeconds(1f);
        ChangeCamera.instance.ChangeToCamera(defaultCam);
        doctorIcon.SetActive(false);
        AEDIcon.SetActive(true);
        dialog.TriggerDialog();
    }

    private IEnumerator Pads()
    {
        yield return new WaitForSeconds(3f);
        pads.transform.DOLocalMove(new Vector3(0.007f, 3.29f, 2.533f), 1f);
    }


}
