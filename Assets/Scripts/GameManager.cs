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
            currentState = GameState.House;
        }
        else if(sceneName == "2 - Odzivnost")
        {
            currentState = GameState.OdzivnostZacetek;
        }
        else if (sceneName == "3 - Dihanje")
        {
            currentState = GameState.DihanjeZacetek;
        }
        else if (sceneName == "4 - CPR")
        {
            currentState = GameState.HandPositions;
        }
        else if (sceneName == "5 - AED")
        {
            currentState = GameState.AED;
        }


        UpdateGameState(currentState);
    }


    public void UpdateGameState(GameState newState)
    {
        State = newState;

        switch (newState)
        {
            case GameState.House:
                break;
            case GameState.IntroAnimation:
                break;
            case GameState.Footsteps:
                break;
            case GameState.Varnost:
                break;
            case GameState.VarnostKoncano:
                break;
            case GameState.OdzivnostZacetek:
                break;
            case GameState.OsebaOdzivna:
                break;
            case GameState.PremakniZrtev:
                break;
            case GameState.Odzivnost:
                break;
            case GameState.OdzivnostGlasnost:
                break;
            case GameState.OdzivnostKoncano:
                break;
            case GameState.DihanjeZacetek:
                break;
            case GameState.ChinLift:
                break;
            case GameState.MouthCheck:
                break;
            case GameState.MouthInside:
                break;
            case GameState.CheckBreathing:
                break;
            case GameState.CallHelp:
                break;
            case GameState.Call112:
                break;
            case GameState.DihanjeKoncano:
                break;
            case GameState.HandPositions:
                break;
            case GameState.CPRPositions:
                break;
            case GameState.LinePositions:
                break;
            case GameState.CPR:
                break;
            case GameState.CPRKira:
                break;
            case GameState.CPRKoncano:
                break;
            case GameState.AED:
                break;
            case GameState.VolumeQuestion:
                break;
            case GameState.AEDShock:
                break;
            case GameState.AEDKoncano:
                break;
            case GameState.Depth:
                break;
            case GameState.Meatball:
                break;
            case GameState.BPM:
                break;
            case GameState.SendForAED:
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
    House,
    Footsteps,
    IntroAnimation,
    Varnost,
    /* --- 2 - ODZIVNOST --- */
    VarnostKoncano,
    OdzivnostZacetek,
    OsebaOdzivna,
    PremakniZrtev,
    Odzivnost,
    OdzivnostGlasnost,
    OdzivnostKoncano,
    /* --- 3 - DIHANJE IN KLIC NA POMOC --- */
    DihanjeZacetek,
    ChinLift,
    MouthCheck,
    MouthInside,
    Meatball,
    CheckBreathing,
    CallHelp,
    Call112,
    SendForAED,
    DihanjeKoncano,
    /* --- 4 - CPR --- */
    HandPositions,
    CPRPositions,
    Depth,
    LinePositions,
    CPR,
    BPM,
    VolumeQuestion,
    CPRKira,
    CPRKoncano,
    /* --- 5 - AED --- */
    AED,
    AEDShock,
    AEDKoncano

}