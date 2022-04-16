using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchSceneFade : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Switcher", 5f);
    }

    // Update is called once per frame
  void Switcher()
    {
        StartCoroutine(GameObject.FindObjectOfType<Fader>().FadeAndLoadScene(Fader.FadeDirection.In, "1 - Varnost"));
    }
}
