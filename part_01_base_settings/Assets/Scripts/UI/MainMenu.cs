using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject menuBackground;
    public PlayerController playerController;

    private void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    public void SaveGame()
    {
        playerController.savePlayer();
    }
    
    public void LoadGame()
    {
        playerController.loadPlayer();
    }
    public void playGame()
    {
        Debug.Log("MainMenu > PlayGame");
        Resume();
        SceneManager.LoadScene("MainScene");
    }
    
    public void quitGame()
    {
        Debug.Log("MainMenu > Quit");
        Application.Quit();
    }
    
    public void loadMenu()
    {
        Debug.Log("PlayGame > MainMenu");
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public bool isGamePaused()
    {
        return gameIsPaused;
    }
    
    public void Resume()
    {
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(false);
            menuBackground.SetActive(false);
        }

        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        menuBackground.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }
}