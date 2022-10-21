using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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

    public void switcher(Camera second_Camera)
    {
        main_Camera.transform.DOMove(second_Camera.transform.position, 3f).SetEase(Ease.OutExpo);
        main_Camera.transform.DORotate(second_Camera.transform.rotation.eulerAngles, 3f).SetEase(Ease.OutExpo);
    }
    public void switcher(Camera second_Camera, float speed)
    {
        main_Camera.transform.DOMove(second_Camera.transform.position, speed).SetEase(Ease.OutExpo);
        main_Camera.transform.DORotate(second_Camera.transform.rotation.eulerAngles, speed).SetEase(Ease.OutExpo);
    }
    public void ChangeToCamera(Camera second_Camera, Camera third_camera)
    {
        main_Camera.transform.DOMove(second_Camera.transform.position, 3f).SetEase(Ease.InExpo);
        main_Camera.transform.DORotate(second_Camera.transform.rotation.eulerAngles, 3f).SetEase(Ease.InExpo).OnComplete(() => switcher(third_camera));

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
        //StartCoroutine(switchCamera(second_Camera));
        main_Camera.transform.DOMove(second_Camera.transform.position, 1.2f).SetEase(Ease.InOutSine);
        main_Camera.transform.DORotate(second_Camera.transform.rotation.eulerAngles, 1.2f).SetEase(Ease.InOutSine);
    }

    public void ChangeToCamera(Camera second_Camera, float speed)
    {
        //StartCoroutine(switchCamera(second_Camera));
        main_Camera.transform.DOMove(second_Camera.transform.position, speed).SetEase(Ease.InOutSine);
        main_Camera.transform.DORotate(second_Camera.transform.rotation.eulerAngles, speed).SetEase(Ease.InOutSine);
    }
    public void ChangeToCameraSlow(Camera second_Camera)
    {
        main_Camera.transform.DOMove(second_Camera.transform.position, 2f).SetEase(Ease.InOutSine);
        main_Camera.transform.DORotate(second_Camera.transform.rotation.eulerAngles, 2f).SetEase(Ease.InOutSine);
    }

    public void ChangeToCamera(Camera second_Camera, Camera third_camera, float speed)
    {
        main_Camera.transform.DOMove(second_Camera.transform.position, speed).SetEase(Ease.InExpo);
        main_Camera.transform.DORotate(second_Camera.transform.rotation.eulerAngles, speed).SetEase(Ease.InExpo).OnComplete(() => switcher(third_camera, speed));

    }
}
