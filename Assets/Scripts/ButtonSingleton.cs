using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSingleton : MonoBehaviour
{
   public static ButtonSingleton instance;
   public bool leftShoulder, rightShoulder;

    private void Awake()
    {
        leftShoulder = false;
        rightShoulder = false;
        instance = this;
    }
}
