using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchSceneFade : MonoBehaviour
{
    // Start is called before the first frame update
    public string sceneName;
    public float TimeToExecute = 0.0f;


    void Start()
    {
        Scene scene = SceneManager.GetActiveScene();

        // Check if the name of the current Active Scene is your first Scene.
        if (scene.name == "InfoHouse")
        {
            SwitcherScene(sceneName);
        }
    }

    // Update is called once per frame
    IEnumerator Switcher(string sceneName)
    {
        //FindObjectOfType<AudioManager>().Play("ButtonClick");
        yield return new WaitForSeconds(TimeToExecute);
        StartCoroutine(GameObject.FindObjectOfType<Fader>().FadeAndLoadScene(Fader.FadeDirection.In, sceneName));
        
    }

    public void SwitcherScene(string sceneName)
    {
        StartCoroutine(Switcher(sceneName));
    }
}
