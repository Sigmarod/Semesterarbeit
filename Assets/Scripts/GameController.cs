using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public bool gameIsPaused;
    public void pauseGame(){
        Time.timeScale = 0;
    }
}
