using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class VerjetnostPrezivetja : MonoBehaviour
{
    // Start is called before the first frame update

    private int probability = 100;
    public TextMeshProUGUI t;

    public GameObject gm;
    void Start()
    {
        ChangeProb();
    }


    public void ChangeProb()
    {
        LeanTween.value(gameObject, 100, 90, 1.2f).setDelay(1.6f).setOnUpdate((float value) =>
        {
            t.text = ((int)value).ToString() +"%";
            //Animate();
        });

    }

    private void Animate()
    {
        LeanTween.scale(gm, new Vector3(5f, 5f, 5f), 0.2f);
        LeanTween.scale(gm, new Vector3(4.227066f, 4.227066f, 4.227066f), 0.2f).setDelay(0.2f);
    }
}
