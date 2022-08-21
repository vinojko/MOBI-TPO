using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AEDShock : MonoBehaviour
{
    private void OnMouseUp()
    {
        AEDPads.instance.clicked = true;
    }
}
