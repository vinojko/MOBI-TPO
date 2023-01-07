using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class LungButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    public bool buttonPressed;

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Pressed");
        CPR.instance.released = false;
        CPR.instance.LungFillStart();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("Released");
        CPR.instance.LungFillEnd();
    }
}