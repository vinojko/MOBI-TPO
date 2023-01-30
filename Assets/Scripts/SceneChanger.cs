using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    // Start is called before the first frame update
    public void ChangeScene(string scene)
    {
        StartCoroutine(ChangeSceneFade(scene));
    }

    private IEnumerator ChangeSceneFade(string scene)
    {
        FaderMouth.instance.FadeIn();
        FindObjectOfType<AudioManager>().FadeOut("MainMenu");
        yield return new WaitForSeconds(0.1f);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
