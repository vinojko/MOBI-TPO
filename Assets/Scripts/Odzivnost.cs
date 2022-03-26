using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Odzivnost : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject leftShoulder, rightShoulder;

    bool showShoulders = false;


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
        if(state == GameState.Odzivnost)showShoulders = true;
    }

    void Start()
    {
        leftShoulder.SetActive(false);
        rightShoulder.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(showShoulders == true)
        {
            Invoke(nameof(ShowShoulders), 3);
        }
    }

    private void ShowShoulders()
    {
        leftShoulder.SetActive(true);
        rightShoulder.SetActive(true);
    }
}
