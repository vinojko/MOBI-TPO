using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroDialog : MonoBehaviour
{
    // Start is called before the first frame update
    public DialogTrigger dialogTrigger;


    void Start()
    {
        Invoke("StartDialog", 4.4f);
    }

    void StartDialog()
    {
        dialogTrigger.TriggerDialog();
    }

}
