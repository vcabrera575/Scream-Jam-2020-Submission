using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public int candy = 0;
    public float gameTimer = 100f;
    public float followerWaitTime = 15f;
    public string message = "";

    float gameStartTime = 0f;

    // Game states
    bool inProgress = true;
    bool gameEnded = false;
    bool timerEnded = false;
    public bool followPlayer = false;

    void Start()
    {
        gameStartTime = gameTimer;
        inProgress = true;
        gameEnded = false;
        timerEnded = false;
        followPlayer = false;
    }

    // Update is called once per frame
    void Update()
    {
        //count down timer
        gameTimer -= Time.deltaTime;

        if (gameTimer <= gameStartTime - followerWaitTime)
            followPlayer = true;

    }

    //get candies
    public void GetCandy(int amount)
    {
        candy += amount;
    }

    // Set message that appears on screen
    public void SetMessage(string newMessage)
    {
        message = newMessage;
    }

    public void Caught()
    {
        gameEnded = true;
    }
}
