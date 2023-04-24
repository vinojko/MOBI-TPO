using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTimeAll : MonoBehaviour
{
    private float startTime; // The time when the current scene started loading
    private List<float> loadingTimes = new List<float>(); // A list to store the loading times of each scene

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene loaded: " + scene.name);
    }
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        StartCoroutine(LoadScenesAsync());

    }

    IEnumerator LoadScenesAsync()
    {
        // Load each scene sequentially
        yield return LoadSceneAsync("1 - Varnost");
        yield return LoadSceneAsync("2 - Odzivnost");
        yield return LoadSceneAsync("3 - Dihanje");
        yield return LoadSceneAsync("4 - CPR");
        yield return LoadSceneAsync("5 - AED");

        // Log the average loading time for each scene
        Debug.Log("Average loading times:");
        for (int i = 0; i < loadingTimes.Count; i++)
        {
            Debug.Log("Scene " + (i + 1) + ": " + loadingTimes[i] + " seconds");
        }
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        startTime = Time.realtimeSinceStartup;
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        float loadTime = Time.realtimeSinceStartup - startTime;
        loadingTimes.Add(loadTime);
        Debug.Log("Time to load " + sceneName + ": " + loadTime + " seconds");
    }
}