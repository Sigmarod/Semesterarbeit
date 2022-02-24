using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameController : MonoBehaviour
{
    public bool gameIsPaused = false;
    public GunBehaviour gunBehaviour;
    public GameObject pausedGame;
    public GameObject normalUI;
    public Slider slider;
    public RocketLauncherBehaviour rocketLauncherBehaviour;

    private void Start()
    {
        Debug.Log(slider);
    }
    private void Update()
    {
        if (gameIsPaused)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                unpauseGame();
                gameIsPaused = false;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                pauseGame(false);
                gameIsPaused = true;
            }
        }
    }
    public void pauseGame(bool GameOver)
    {
        if (!GameOver)
        {
            pausedGame.SetActive(true);
            normalUI.SetActive(false);
            slider.value = PlayerPrefs.GetFloat("sensitivity");
            UnityEngine.Cursor.lockState = CursorLockMode.None;
        }

        Time.timeScale = 0;
        gameIsPaused = true;
        gunBehaviour.pauseGame();
        rocketLauncherBehaviour.pauseGame();
        UnityEngine.Cursor.lockState = CursorLockMode.None;
    }

    public void unpauseGame()
    {
        PlayerPrefs.SetFloat("sensitivity", slider.value);
        pausedGame.SetActive(false);
        normalUI.SetActive(true);
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
        gameIsPaused = false;
        gunBehaviour.pauseGame();
        rocketLauncherBehaviour.pauseGame();
        
    }

    public void resetGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void backToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("StartMenu");
    }
}
