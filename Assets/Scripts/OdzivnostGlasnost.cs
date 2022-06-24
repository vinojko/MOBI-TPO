using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OdzivnostGlasnost : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject MicUI;
    public float threshold = 0.30f;

    public DialogTrigger dialogTrigger;
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
        if(state == GameState.OdzivnostGlasnost)
        {
            StartAnimation();
        }
        else
        {
            EndAnimation();
        }

    }
    private void Update()
    {
        Debug.Log(MicInput.MicLoudness);
  
        if(MicInput.MicLoudness >= threshold && (GameManager.currentState == GameState.OdzivnostGlasnost))
        {
            Debug.Log("Uporabnik je prekoracil glasnost");
            dialogTrigger.TriggerDialog();
            GameManager.instance.UpdateGameState(GameState.OdzivnostKoncano);
        }
    }

    private void StartAnimation()
    {
        LeanTween.moveLocal(MicUI, new Vector3(-300f, -850f, 0f), 1.7f).setDelay(0.2f).setEase(LeanTweenType.easeOutElastic);
    }

    private void EndAnimation()
    {
        LeanTween.moveLocal(MicUI, new Vector3(-1200f, -850f, 0f), 1.7f).setDelay(0.2f).setEase(LeanTweenType.easeOutElastic);
    }
}
