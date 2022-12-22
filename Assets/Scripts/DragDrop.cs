using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDrop : MonoBehaviour
{
    Vector3 offset;
    public string destinationTag = "DropArea";

    private Vector3 startPosition;
    public GameObject padLeft, padRight;

    private bool padLeftSet, padRightSet;

    void OnMouseDown()
    {
        offset = transform.position - MouseWorldPosition();
        transform.GetComponent<Collider>().enabled = false;
        startPosition = transform.position;

        FindObjectOfType<AudioManager>().Play("StickerRip");

    }

    void OnMouseDrag()
    {
        transform.position = MouseWorldPosition() + offset;
    }

    void Start()
    {
        padLeft.SetActive(false);
        padRight.SetActive(false);
        startPosition = transform.position;

        padLeftSet = false;
        padRightSet = false;
    }

    void OnMouseUp()
    {
        var rayOrigin = Camera.main.transform.position;
        var rayDirection = MouseWorldPosition() - Camera.main.transform.position;
        RaycastHit hitInfo;
        if (Physics.Raycast(rayOrigin, rayDirection, out hitInfo))
        {
            if (hitInfo.transform.CompareTag(destinationTag))
            {
                // Za center - transform.position = hitInfo.transform.position;

                if (destinationTag == "DropLeft")
                {
                    padLeft.SetActive(true);
                    AEDPads.instance.leftPadSet = true;
                    AEDPads.instance.PadsSet();
                    FindObjectOfType<AudioManager>().Play("StickerPlace");

                }
                else if (destinationTag == "DropRight") {

                    padRight.SetActive(true);
                    AEDPads.instance.rightPadSet = true;
                    AEDPads.instance.PadsSet();
                    FindObjectOfType<AudioManager>().Play("StickerPlace");
                } 

                gameObject.SetActive(false);
            }
            else
            {
                transform.position = startPosition;
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
}