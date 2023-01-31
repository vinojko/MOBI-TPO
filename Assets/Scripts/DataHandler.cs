using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataHandler : MonoBehaviour
{
    // Start is called before the first frame update

    public DataManager dataManager;

    public Button  odzivnostButton, dihanjeButton, CPRButton, AEDButton;
    public GameObject odzivnostLock, dihanjeLock, CPRLock, AEDLock;
    public GameObject tutorial;
    void Start()
    {
        dataManager.Load();

        odzivnostButton.interactable = false;
        dihanjeButton.interactable = false;
        CPRButton.interactable = false;
        AEDButton.interactable = false;
        FindObjectOfType<AudioManager>().Play("City");

        if (!dataManager.data.showTutorial)
        {
            tutorial.SetActive(false);
        }

        if (dataManager.data.odzivnost)
        {
            odzivnostButton.interactable = true;
            odzivnostLock.SetActive(false);
        }
        if (dataManager.data.dihanje)
        {
            dihanjeButton.interactable = true;
            dihanjeLock.SetActive(false);
        }
        if (dataManager.data.cpr)
        {
            CPRButton.interactable = true;
            CPRLock.SetActive(false);
        }
        if (dataManager.data.aed)
        {
            AEDButton.interactable = true;
            AEDLock.SetActive(false);
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
