using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public bool gameIsPaused = false;
    public GunBehaviour gunBehaviour;
    public RocketLauncherBehaviour rocketLauncherBehaviour;
    private void Update() {
        if(gameIsPaused){
            if(Input.GetKeyDown(KeyCode.Escape))
        {
            unpauseGame();
            gameIsPaused = false;
        }
        }else{
            if(Input.GetKeyDown(KeyCode.Escape))
        {
            pauseGame();
            gameIsPaused = true;
        }
        }  
    }
    public void pauseGame(){
        Time.timeScale = 0;
        gameIsPaused = true;
        gunBehaviour.pauseGame();
        rocketLauncherBehaviour.pauseGame();
        Cursor.lockState = CursorLockMode.None;
    }

    public void unpauseGame(){
        Time.timeScale = 1;
        gameIsPaused = false;
        gunBehaviour.pauseGame();
        rocketLauncherBehaviour.pauseGame();
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void resetGame(){
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    public void backToMenu(){
        Time.timeScale = 1;
        SceneManager.LoadScene("StartMenu");
    }
}
