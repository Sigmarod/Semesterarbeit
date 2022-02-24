using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenuManager : MonoBehaviour
{
    public void endGame(){
        Application.Quit();
    }
    
    public void startGame(){
        SceneManager.LoadScene("Main"); 
    }

    public void openGitHub(){
        Application.OpenURL("https://github.com/Sigmarod/Semesterarbeit");
    }

    public void openCredits(){
        SceneManager.LoadScene("Credits");
    }

    public void openTutorial(){
        SceneManager.LoadScene("Tutorial");
    }


}
