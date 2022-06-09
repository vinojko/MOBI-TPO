using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndressHans : MonoBehaviour
{

    public GameObject HansDressed, HansUndressed;
    // Start is called before the first frame update
   public void Undress()
    {
        if(GameManager.currentState == GameState.ChinLift)
        {
            HansDressed.SetActive(false);
            HansUndressed.SetActive(true);

            GameManager.instance.UpdateGameState(GameState.ChinLift);
        }

    }
    
}
