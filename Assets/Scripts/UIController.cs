using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    int timer =  5;
    int score = 0;
    public Text timerText;
    public Text scoreText;
    public Text endGameScoreText;
    public GameObject duringGame;
    public GameObject endGame;
    public GameObject gameManager;

    void Start()
    {
        timerFunction();
    }

    public void timerFunction()
    {
        if (timer <= 60 && timer > 0)
        {
            StartCoroutine(countDown());
        }
        else
        {

            gameManager.GetComponent<GameController>().pauseGame();
            duringGame.SetActive(false);
            endGame.SetActive(true);
            endGameScoreText.text = score.ToString();
        }
    }

    IEnumerator countDown()
    {
        yield return new WaitForSeconds(1);
        // trigger the stop animation events here
        timer -= 1;
        timerText.text = timer.ToString();
        Debug.Log("timer - 1");
        timerFunction();

    }

    public void addScore()
    {
        score += 1;
        scoreText.text = score.ToString();
    }

}
