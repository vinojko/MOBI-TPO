using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextLevelAnim : MonoBehaviour
{
    public CanvasGroup panel;
    public GameObject nextLevelUI;

    public DataManager dataManager;
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
        if (state == GameState.OdzivnostKoncano || state == GameState.VarnostKoncano)
        {
            LeanTween.alphaCanvas(panel, 1f, 1f);
            NextLevelAnimation();
            
            Debug.Log("ANIMACIJA");
        }


             


    }

    private void NextLevelAnimation()
    {
        LeanTween.moveLocal(nextLevelUI, new Vector3(0f, 0f, 0f), 1.7f).setDelay(0.7f).setEase(LeanTweenType.easeOutExpo);
    }

}
