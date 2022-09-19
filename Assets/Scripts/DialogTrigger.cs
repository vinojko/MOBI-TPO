using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    public Dialog dialog;

    public void TriggerDialog()
    {
        if (FindObjectOfType<DialogManager>() != null)
        {
            FindObjectOfType<DialogManager>().startDialog(dialog);
        }
        else
        {
            FindObjectOfType<DialogFirstTime>().startDialog(dialog);
        }
        

    }
}
