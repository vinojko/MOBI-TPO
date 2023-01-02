using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndressHans : MonoBehaviour
{

    public GameObject HansDressed, HansUndressed, nipples;
    [SerializeField]
    DialogTrigger undressHanzDialog;
    // Start is called before the first frame update
    void Awake()
    {
        GameManager.OnGameStateChanged += GameManagerOnStateChanged;
        nipples.SetActive(false);
    }


    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManagerOnStateChanged;
    }

    private void GameManagerOnStateChanged(GameState state)
    {

        if (state == GameState.DihanjeZacetek)
        {

            undressHanzDialog.TriggerDialog();
            
        }
    }

    public void Undress()
    {
        if(GameManager.currentState == GameState.DihanjeZacetek)
        {
            HansDressed.SetActive(false);
            HansUndressed.SetActive(true);
            nipples.SetActive(true);

            GameManager.instance.UpdateGameState(GameState.ChinLift);
        }

    }
    
}
