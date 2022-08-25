using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject pauseMenu;
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
            if (Input.GetKey(KeyCode.Escape))
            {
                PauseGame();

                return;
            }
        
    }
    void PauseGame()
    {
        FindObjectOfType<AudioManager>().Play("ButtonClick");
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        FindObjectOfType<AudioManager>().Play("ButtonClick");
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;

    }

    public void Home()
    {
        FindObjectOfType<AudioManager>().Play("ButtonClick");
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
    }

    public void Replay()
    {
        FindObjectOfType<AudioManager>().Play("ButtonClick");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }
}
