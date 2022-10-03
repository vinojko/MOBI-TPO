using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeState : MonoBehaviour
{
    public void ChangeStateTo(string state)
    {

        if(state.Equals("Varnost")) GameManager.instance.UpdateGameState(GameState.Varnost);
        if (state.Equals("Odzivnost")) GameManager.instance.UpdateGameState(GameState.Odzivnost);
        if (state.Equals("PremakniZrtev")) GameManager.instance.UpdateGameState(GameState.PremakniZrtev);
        if (state.Equals("OsebaOdzivna")) GameManager.instance.UpdateGameState(GameState.OsebaOdzivna);

    }
}
