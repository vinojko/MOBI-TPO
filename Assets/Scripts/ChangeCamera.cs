using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCamera : MonoBehaviour
{
    public static ChangeCamera instance;
    public Camera main_Camera;


    void Awake()
    {
        instance = this;
    }

    public IEnumerator switchCamera(Camera second_Camera)
    {

        var animSpeed = 1.5f;

        Vector3 pos = main_Camera.transform.position;
        Quaternion rot = main_Camera.transform.rotation;

        float progress = 0.0f;  //This value is used for LERP

        while (progress < 1.0f)
        {
            main_Camera.transform.position = Vector3.Lerp(pos, second_Camera.transform.position, progress);
            main_Camera.transform.rotation = Quaternion.Lerp(rot, second_Camera.transform.rotation, progress);
            yield return new WaitForEndOfFrame();
            progress += Time.deltaTime * animSpeed;
        }

        //Set final transform
        main_Camera.transform.position = second_Camera.transform.position;
        main_Camera.transform.rotation = second_Camera.transform.rotation;
    }
    public IEnumerator switchCameraSlow(Camera second_Camera)
    {

        var animSpeed = 0.8f;

        Vector3 pos = main_Camera.transform.position;
        Quaternion rot = main_Camera.transform.rotation;

        float progress = 0.0f;  //This value is used for LERP

        while (progress < 1.0f)
        {
            main_Camera.transform.position = Vector3.Lerp(pos, second_Camera.transform.position, progress);
            main_Camera.transform.rotation = Quaternion.Lerp(rot, second_Camera.transform.rotation, progress);
            yield return new WaitForEndOfFrame();
            progress += Time.deltaTime * animSpeed;
        }

        //Set final transform
        main_Camera.transform.position = second_Camera.transform.position;
        main_Camera.transform.rotation = second_Camera.transform.rotation;
    }

    public void ChangeToCamera(Camera second_Camera)
    {
        StartCoroutine(switchCamera(second_Camera));
    }
    public void ChangeToCameraSlow(Camera second_Camera)
    {
        StartCoroutine(switchCameraSlow(second_Camera));
    }
}
