using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    public void back(){
        SceneManager.LoadScene("StartMenu"); 
    }

}
