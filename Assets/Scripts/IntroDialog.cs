using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroDialog : MonoBehaviour
{
    // Start is called before the first frame update
    public DialogTrigger dialogTrigger;


    void Start()
    {
        Invoke("StartDialog",0.1f);
    }

    void StartDialog()
    {
        dialogTrigger.TriggerDialog();
    }

}
