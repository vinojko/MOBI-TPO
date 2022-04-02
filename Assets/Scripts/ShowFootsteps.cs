using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowFootsteps : MonoBehaviour
{
    [SerializeField] private GameObject HanzFootsteps, OvenFootsteps;

    bool showOvenFootsteps = true;
    bool showHanzFootsteps = true;

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
   
        HanzFootsteps.SetActive(state == GameState.Footsteps);
        OvenFootsteps.SetActive(state == GameState.Footsteps);
 

    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
