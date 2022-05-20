using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowFootsteps : MonoBehaviour
{
    [SerializeField] private GameObject HanzFootsteps, OvenFootsteps;

    void Awake()
    {
        GameManager.OnGameStateChanged += GameManagerOnStateChanged;
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManagerOnStateChanged;
    }

    private void GameManagerOnStateChanged(GameState state)
    {
        Debug.Log("OnChange happened!");
   
        HanzFootsteps.SetActive(state == GameState.OdzivnostZacetek || state == GameState.Footsteps );
        //if(OvenFootsteps != null) OvenFootsteps.SetActive(state == GameState.Footsteps);
        //if (HanzFootsteps != null) HanzFootsteps.SetActive(state == GameState.Footsteps);

    }
}
