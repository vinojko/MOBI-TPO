using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostHands : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject ghostHands;
    private Vector3 initHands;
    void Start()
    {
        initHands = new Vector3(3.03889f, 6.0774f, 3.798612f);
    }

    // Update is called once per frame

    private void GameManagerOnStateChanged(GameState state)
    {

        if (state != GameState.AED)
        {
  
            
        }
    }
    private void HandsAnimation()
    {
        LeanTween.scale(ghostHands, initHands - new Vector3(0.2f, 0.2f, 0.2f), 0.2f);
        LeanTween.scale(ghostHands, initHands, 0.2f).setDelay(0.2f);
    }
}
