using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuizResults : MonoBehaviour
{
    // Start is called before the first frame update
   public static QuizResults instance;
    public bool varnost, odzivnost, dihanje, kpo , aed;
    public CanvasGroup text;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        varnost = false;
        odzivnost = false;
        dihanje = false;
        kpo = false;
        aed = false;
        
    }


    public IEnumerator End()
    {
        if (varnost && odzivnost && dihanje && kpo && aed)
        {
            //LeanTween.reset();
            LeanTween.value(gameObject, 0f, 1f, 0.5f).setOnUpdate((value) =>
            {
                text.alpha = value;
            });

            yield return new WaitForSeconds(2f);

            SceneManager.LoadScene("MainMenu");
        }
        yield return null;

    }
}
