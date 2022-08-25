using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndressHans : MonoBehaviour
{

    public GameObject HansDressed, HansUndressed;
    // Start is called before the first frame update

    private void Awake()
    {
        GameManager.instance.UpdateGameState(GameState.DihanjeZacetek);
    }
    public void Undress()
    {
        if(GameManager.currentState == GameState.DihanjeZacetek)
        {
            HansDressed.SetActive(false);
            HansUndressed.SetActive(true);

            GameManager.instance.UpdateGameState(GameState.ChinLift);
        }

    }
    
}
