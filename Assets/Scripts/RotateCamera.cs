using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{

    public float Speed = 5;
    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles += Speed * new Vector3(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"),0);
    }
}
