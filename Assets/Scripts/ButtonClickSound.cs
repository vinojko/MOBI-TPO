using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClickSound : MonoBehaviour
{
    // Start is called before the first frame update
    public void Click(string sound)
    {
        FindObjectOfType<AudioManager>().Play(sound);
        FindObjectOfType<AudioManager>().Stop("Theme");
    }
}
