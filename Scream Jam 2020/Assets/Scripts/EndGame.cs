﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public static int currentScore;
    public static int highScore;
    public GameController gameController;

    private void GameEnd()
    {
        //unlock cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        //get current score
        currentScore = gameController.highScore + gameController.candy;
        //get highscore
        highScore = PlayerPrefs.GetInt("highscore", 0);

        //if beat highscore, replace it
        if (currentScore > highScore)
        {
            PlayerPrefs.SetInt("highscore", currentScore);
        }
        //save current score
        PlayerPrefs.SetInt("score", currentScore);
        //go to win screen
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    private void Update()
    {
        Vector3 player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;
        float dist = Vector3.Distance(player, transform.position);
        Debug.Log("High Score: " + gameController.highScore);
        if (dist < 3f && (gameController.candy > 0 || gameController.highScore > 0))
            GameEnd();
    }
}
