using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PremakniZrtev : MonoBehaviour
{
    public Animator animator;
    int BlendHash;

    void Start()
    {

        BlendHash = Animator.StringToHash("Blend");
    }
    public IEnumerator MoveVictim()
    {
        var animSpeed = 0.1f;
        float progress = 0.0f;  

        while (progress < 1.0f)
        {
            animator.SetFloat(BlendHash, progress);
            yield return new WaitForEndOfFrame();
            progress += Time.deltaTime * animSpeed;
        }

    }
    public void MoveTheVictim()
    {
       if(GameManager.currentState == GameState.PremakniZrtev)
        StartCoroutine(MoveVictim());
    }

}
