using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AEDPads : MonoBehaviour
{
    // Start is called before the first frame update

    public static AEDPads instance;
    public DialogTrigger analysisDialog;
    public bool leftPadSet, rightPadSet = false;

    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PadsSet()
    {
        if(leftPadSet && rightPadSet)
        {
            analysisDialog.TriggerDialog();
        }
        
    }
}
