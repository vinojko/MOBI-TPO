using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChinLift : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject chinLift, headLift;
    public Camera chinCamera;
    [SerializeField] private Animator animator;


    private void Start()
    {
        chinLift.SetActive(false);
        headLift.SetActive(false);
    }
    void Awake()
    {
        GameManager.OnGameStateChanged += GameManagerOnStateChanged;
    }

    private void Update()
    {
        if (ButtonSingleton.instance.leftShoulder && GameManager.currentState == GameState.ChinLift)
        {
            Debug.Log("Chin correct");
            animator.SetBool("playChin", true);
            GameManager.instance.UpdateGameState(GameState.MouthCheck);
        }
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManagerOnStateChanged;
    }

    private void GameManagerOnStateChanged(GameState state)
    {
        if(state == GameState.ChinLift)
        {
            HandleChinLift();
        }
        
        if(state != GameState.ChinLift)
        {
            headLift.SetActive(false);
            chinLift.SetActive(false);
        }
        
    }

    private IEnumerator ChinLiftCoroutine()
    {
        MoveCamera();
        yield return new WaitForSeconds(1.0f);
        chinLift.SetActive(true);
        headLift.SetActive(true);

    }


    
    private void HandleChinLift()
    {
        StartCoroutine(ChinLiftCoroutine());
    }

    private void MoveCamera()
    {
        ChangeCamera.instance.ChangeToCamera(chinCamera);
    }

    
}
