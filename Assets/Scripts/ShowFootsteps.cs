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
   
        HanzFootsteps.SetActive(state == GameState.Footsteps || state == GameState.VarnostKoncano);
        if(OvenFootsteps != null) OvenFootsteps.SetActive(state == GameState.Footsteps);


    }
}
