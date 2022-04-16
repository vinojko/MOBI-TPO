using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseRotation : MonoBehaviour
{
    // Start is called before the first frame update
    void Update()
    {
        transform.Rotate(0, 3.5f * Time.deltaTime, 0); //rotates 50 degrees per second around z axis
    }

}
