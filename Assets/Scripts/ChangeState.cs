using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeState : MonoBehaviour
{
    public void ChangeStateTo(string state)
    {

        if(state.Equals("Varnost")) GameManager.instance.UpdateGameState(GameState.Varnost);

    }
}
