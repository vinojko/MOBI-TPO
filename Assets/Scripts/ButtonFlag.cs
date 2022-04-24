using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ButtonFlag : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public string whichButton;

    public void OnPointerDown(PointerEventData eventData)
    {

        if (whichButton.Equals("L"))
        {
            ButtonSingleton.instance.leftShoulder = true;
            Debug.Log("lEFT SHOULDER PRESSED");
        }
        else
        {
            ButtonSingleton.instance.rightShoulder = true;
            Debug.Log("right SHOULDER PRESSED");
        }
 
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (whichButton.Equals("L"))
        {
            ButtonSingleton.instance.leftShoulder = false;
            Debug.Log("left SHOULDER released");
        }
        else
        {
            ButtonSingleton.instance.rightShoulder = false;
            Debug.Log("right SHOULDER released");
        }
    }

}