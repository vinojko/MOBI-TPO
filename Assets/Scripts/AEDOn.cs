using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AEDOn : MonoBehaviour
{
    // Start is called before the first frame update

    public string destinationTag = "AEDOn";
    public Material AEDOnMaterial;

    public Camera AEDCam;
    public Camera defaultCam;

    public GameObject doctorIcon, AEDIcon;

    public DialogTrigger dialog;
    void Start()
    {
        StartCoroutine(MoveCamera());
        doctorIcon.SetActive(true);
        AEDIcon.SetActive(false);
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
        Debug.Log("aed ON");
        var rayOrigin = Camera.main.transform.position;
        var rayDirection = MouseWorldPosition() - Camera.main.transform.position;
        RaycastHit hitInfo;
        if (Physics.Raycast(rayOrigin, rayDirection, out hitInfo))
        {
            if (hitInfo.transform.CompareTag(destinationTag))
            {
               Debug.Log("aed ON");
               AEDOnMaterial.EnableKeyword("_EMISSION");
               ChangeCamera.instance.ChangeToCamera(defaultCam);

                doctorIcon.SetActive(false);
                AEDIcon.SetActive(true);

                dialog.TriggerDialog();



            }

        }


        transform.GetComponent<Collider>().enabled = true;
    }

    Vector3 MouseWorldPosition()
    {
        var mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mouseScreenPos);
    }

    private IEnumerator MoveCamera()
    {

        yield return new WaitForSeconds(4.5f);
        ChangeCamera.instance.ChangeToCamera(AEDCam);
    }
}
