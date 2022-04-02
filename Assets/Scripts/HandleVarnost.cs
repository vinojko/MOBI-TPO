using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleVarnost : MonoBehaviour
{
    public float delayTime;
    public GameObject button;
    private Vector3 posB;
    private Vector3 temp = new Vector3(-2.3614f, 1.625f, -22.231f);
    
    void Start(){

        posB = button.transform.position;
        posB += temp;

    }

    public IEnumerator HandleButton()
    {
 
        yield return new WaitForSeconds(0); // start at time X
        float startTime = Time.time; // Time.time contains current frame time, so remember starting point
        while (Time.time - startTime <= 1)
        { // until one second passed
            button.transform.position = Vector3.Lerp(button.transform.position, posB, Time.time - startTime); // lerp from A to B in one second
            yield return 1; // wait for next frame
        }
    }

    public void HandleButtonOff(){
        StartCoroutine(HandleButton());
    }
}