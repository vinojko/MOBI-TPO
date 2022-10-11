using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VPManager : MonoBehaviour
{
    // Start is called before the first frame update

    public int vp;
    public static VPManager instance;
    public DataManager dataManager;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (sceneName == "1 - Varnost")
        {
            dataManager.Load();
            dataManager.data.verjetnostPrezivetja = 90;
            dataManager.Save();
        }

        // TESTIRANJE
        if (sceneName == "5 - AED")
        {
            dataManager.Load();
            dataManager.data.verjetnostPrezivetja = 90;
            dataManager.Save();
        }

        dataManager.Load();
        vp = dataManager.data.verjetnostPrezivetja;
        dataManager.Save();
    }

    public void Decrease()
    {
        if ((vp - 9) > 0)
        {
            vp -= 9;
        }

        dataManager.data.verjetnostPrezivetja = vp;
        dataManager.Save();
    }
}
