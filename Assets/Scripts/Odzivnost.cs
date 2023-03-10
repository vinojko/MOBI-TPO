using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Odzivnost : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject leftShoulder, rightShoulder;

    bool showShoulders = false;

    public DialogTrigger odzivnostDialog;

    public DialogTrigger zavpijteDialog;


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
        if (state == GameState.Odzivnost)
        {
            Invoke(nameof(ShowShoulders), 3);
            odzivnostDialog.TriggerDialog();
        }
        else if(state == GameState.OdzivnostGlasnost){
            Invoke(nameof(HideShoulders), 0.1f);
        }

    }

    void Start()
    {
        leftShoulder.SetActive(false);
        rightShoulder.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (ButtonSingleton.instance.leftShoulder &&  ButtonSingleton.instance.rightShoulder && ShakeCheck.instance.isShaken)
        {
            GameManager.instance.UpdateGameState(GameState.OdzivnostGlasnost);
            zavpijteDialog.TriggerDialog();
            ButtonSingleton.instance.leftShoulder = false;
            ButtonSingleton.instance.rightShoulder = false;


        }
    }

    private void ShowShoulders()
    {
        leftShoulder.SetActive(true);
        rightShoulder.SetActive(true);
    }
    private void HideShoulders()
    {
        leftShoulder.SetActive(false);
        rightShoulder.SetActive(false);
    }
}
