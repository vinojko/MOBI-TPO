using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    // Start is called before the first frame update
 

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0.5f* Time.deltaTime, 0);
    }
}
