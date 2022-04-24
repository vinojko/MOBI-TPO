using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        Scene currentScene = SceneManager.GetActiveScene();

        // Retrieve the name of this scene.
        string sceneName = currentScene.name;

        if(sceneName == "1 - Varnost")
        {
            currentState = GameState.Footsteps;
        }
        else if(sceneName == "2 - Odzivnost")
        {
            currentState = GameState.VarnostKoncano;
        }

        UpdateGameState(currentState);
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
            case GameState.OdzivnostGlasnost:
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
    /* --- 1 - VARNOST --- */
    Footsteps,
    Varnost,
    /* --- 2 - ODZIVNOST--- */
    VarnostKoncano,
    PremakniZrtev,
    Odzivnost,
    OdzivnostGlasnost,
    PomocDihanje,
    CPR,
    AED

}