using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class HandleVarnost : MonoBehaviour
{
    public float delayTime;
    public GameObject button;
    private Vector3 temp = new Vector3(-2.3614f, 1.625f, -22.231f);
    public VisualEffect smokeVfx;

    public Camera HanzCamera;
    
    public IEnumerator HandleButton()
    {
 
        yield return new WaitForSeconds(0);

        if (GameManager.currentState == GameState.Varnost)
        {
            float startTime = Time.time;
            while (Time.time - startTime <= 1)
            {   
                //Premaknemo gumb ON/OFF v x os, da zgleda kot da ga premaknemo
                button.transform.position = Vector3.Lerp(button.transform.position, temp, Time.time - startTime); // lerp from A to B in one second
                yield return 1; 
            }
            ChangeCamera.instance.ChangeToCamera(HanzCamera);
            //STATE Varnost je sedaj koncana, NOV STATE
            GameManager.instance.UpdateGameState(GameState.VarnostKoncano);

        }
    }

    public void HandleButtonOff(){
        StartCoroutine(HandleButton());
        smokeVfx.Stop();
    }
}