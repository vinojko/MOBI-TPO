using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameState State;
    public static GameState currentState;

    public static event Action<GameState> OnGameStateChanged;
    void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        currentState = GameState.Footsteps;
        UpdateGameState(GameState.Footsteps);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateGameState(GameState newState)
    {
        State = newState;

        switch (newState)
        {
            case GameState.Footsteps:
                break;
            case GameState.Varnost:
                break;
            case GameState.VarnostKoncano:
                break;
            case GameState.PremakniZrtev:
                break;
            case GameState.Odzivnost:
                break;
            case GameState.PomocDihanje:
                break;
            case GameState.CPR:
                break;
            case GameState.AED:
                break;
        }
        OnGameStateChanged?.Invoke(newState);
        currentState = newState;
        Debug.Log("Curent State: " + currentState);
    }
    
}

public enum GameState
{
    //VARNOST
    Footsteps,
    Varnost,
    VarnostKoncano,
    PremakniZrtev,
    //Konec VARNOST
    Odzivnost,
    PomocDihanje,
    CPR,
    AED

}