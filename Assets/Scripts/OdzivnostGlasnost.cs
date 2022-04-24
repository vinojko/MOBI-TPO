using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OdzivnostGlasnost : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject MicUI;
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
        

    }

    private void AnimationUI()
    {
        LeanTween.moveLocal(MicUI, new Vector3(-300f, -857f, 0f), 0.7f).setDelay(0.2f).setEase(LeanTweenType.easeInElastic);
    }
}
