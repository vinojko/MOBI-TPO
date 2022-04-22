using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PremakniZrtev : MonoBehaviour
{
    public Animator animator;
    int BlendHash;
    public float animSpeed = 1.6f;
    public Camera cam;


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
            Debug.Log(progress);

            progress += Time.deltaTime * animSpeed;
        }


    }
    public void MoveTheVictim()
    {


        StopAllCoroutines();
        if (GameManager.currentState == GameState.PremakniZrtev) StartCoroutine(MoveVictim());
        GameManager.instance.UpdateGameState(GameState.Odzivnost);

    }

    public void MoveCamera()
    {

        ChangeCamera.instance.ChangeToCamera(cam);

    }
}
