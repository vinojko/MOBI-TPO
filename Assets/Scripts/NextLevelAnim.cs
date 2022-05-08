using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextLevelAnim : MonoBehaviour
{
    public GameObject panel;
    public GameObject nextLevelUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
        if (state == GameState.OdzivnostKoncano)
        {
            FadeStart();
        }


    }
    void FadeStart()
    {
        LeanTween.alpha(panel, 1f, 1f).setEase(LeanTweenType.linear);
    }
    void FadeFinished()
    {
        LeanTween.alpha(panel, 0f, 1f).setEase(LeanTweenType.linear).setOnComplete(FadeStart);
    }
}
