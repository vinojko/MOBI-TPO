using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class HandleVarnost : MonoBehaviour
{
    public float delayTime;
    public GameObject button;
    private Vector3 posB;
    private Vector3 temp = new Vector3(-2.3614f, 1.625f, -22.231f);
    public VisualEffect smokeVfx;
    
    void Start(){

        posB = button.transform.position;
        posB += temp;

    }

    public IEnumerator HandleButton()
    {
 
        yield return new WaitForSeconds(0);

        if (GameManager.currentState == GameState.Varnost)
        {
            float startTime = Time.time; // Time.time contains current frame time, so remember starting point
            while (Time.time - startTime <= 1)
            { // until one second passed
                button.transform.position = Vector3.Lerp(button.transform.position, temp, Time.time - startTime); // lerp from A to B in one second
                yield return 1; // wait for next frame
            }
        }// start at time X
 
    }

    public void HandleButtonOff(){
        StartCoroutine(HandleButton());
        smokeVfx.Stop();

    }
}