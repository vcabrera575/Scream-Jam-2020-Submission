using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    float gameStartTime = 0f;
    public float gameTimer = 100f;
    public string message = "";

    public bool followPlayer = false;
    public float followerWaitTime = 15f;
    public float followerSpeed = 2f;

    public int candy = 0;

    // Game states
    bool inProgress = true;
    bool gameEnded = false;
    bool timerEnded = false;

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

        if (gameTimer <= gameStartTime - followerWaitTime || gameTimer <= 0)
            followPlayer = true;
        if (gameTimer < 0)
            followerSpeed += (0.005f);
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
