using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class LungButtonAED : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    public bool buttonPressed;

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Pressed");
        CPRAED.instance.released = false;
        CPRAED.instance.LungFillStart();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("Released");
        CPRAED.instance.LungFillEnd();
    }
}