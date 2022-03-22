using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameState State;

    public static event Action<GameState> OnGameStateChanged;
    void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        UpdateGameState(GameState.Varnost);
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
            case GameState.Varnost:
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
    }
    
}

public enum GameState
{
    Varnost,
    Odzivnost,
    PomocDihanje,
    CPR,
    AED

}