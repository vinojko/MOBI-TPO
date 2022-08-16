using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    // Start is called before the first frame update

    public DataManager dataManager;

    void Awake()
    {
        GameManager.OnGameStateChanged += GameManagerOnStateChanged;

    }




    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManagerOnStateChanged;
    }

    private void GameManagerOnStateChanged(GameState state)
    {

        

        if (state == GameState.VarnostKoncano)
        {
            dataManager.Load();
            dataManager.data.odzivnost = true;
            dataManager.Save();

        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
