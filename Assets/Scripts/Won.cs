using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Won : MonoBehaviour
{

    // Start is called before the first frame update

    public Camera cam1, cam2;
    void Start()
    {
        StartCoroutine(MoveCam());
    }

    private IEnumerator MoveCam()
    {
        yield return new WaitForSeconds(2f);
        ChangeCamera.instance.ChangeToCamera(cam1, cam2, 10f);
    }
}
