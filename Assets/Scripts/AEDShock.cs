using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AEDShock : MonoBehaviour
{
    private void OnMouseUp()
    {
        if(GameManager.currentState == GameState.AEDShock) AEDPads.instance.clicked = true;

    }
}
