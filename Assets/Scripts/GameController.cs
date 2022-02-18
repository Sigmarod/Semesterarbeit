using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public bool gameIsPaused = false;
    public GunBehaviour gunBehaviour;
    public RocketLauncherBehaviour rocketLauncherBehaviour;
    public void pauseGame(){
        Time.timeScale = 0;
        gameIsPaused = true;
        gunBehaviour.pauseGame();
        rocketLauncherBehaviour.pauseGame();
        Cursor.lockState = CursorLockMode.None;
    }

    public void resetGame(){
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
}
