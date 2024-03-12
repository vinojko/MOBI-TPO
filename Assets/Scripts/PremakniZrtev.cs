using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PremakniZrtev : MonoBehaviour
{
    public Animator animator;
    int BlendHash;
    public float animSpeed = 1.6f;
    public Camera cam;
    public DialogTrigger hrbetDialog;


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
        if (state == GameState.PremakniZrtev)
        {
            hrbetDialog.TriggerDialog();

        }

    }

    void Start()
    {

        BlendHash = Animator.StringToHash("Blend");
    }
    public IEnumerator MoveVictim()
    {
        MoveCamera();
        float progress = 0.0f;



        while (progress < 1.0f)
        {
            animator.SetFloat(BlendHash, progress);
            yield return new WaitForEndOfFrame();
            //Debug.Log(progress);

            progress += Time.deltaTime * animSpeed;
        }


    }
    public void MoveTheVictim()
    {


        if (GameManager.currentState == GameState.PremakniZrtev)
        {
            StartCoroutine(MoveVictim());
            //StopAllCoroutines();
            GameManager.instance.UpdateGameState(GameState.DihanjeZacetek);
        }


    }

    public void MoveCamera()
    {

        ChangeCamera.instance.ChangeToCamera(cam);

    }
}
