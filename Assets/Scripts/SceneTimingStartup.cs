using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTimingStartup : MonoBehaviour
{
    private float startTime; // The time when the current scene started loading
    public string sceneName;


    // called first
    void OnEnable()
    {
        Debug.Log("OnEnable called");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start()
    {
        startTime = Time.realtimeSinceStartup;
        StartCoroutine(LoadNextSceneAsync());
    }

    IEnumerator LoadNextSceneAsync()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        float loadTime = Time.realtimeSinceStartup - startTime;
        Debug.Log("Time to load " + scene.name + ": " + loadTime + " seconds");
    }
}