using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    // Game timer management
    float gameStartTime = 0f;
    public float gameTimer = 100f;
    public string message = "";

    // Follower Variables
    public bool followPlayer = false;
    public float followerWaitTime = 15f;
    public float followerSpeed = 8f;

    // Candy management
    public int candy = 0;
    public float candySpeedBoost = 2f;
    public float candySaturation = 34f; // A number that is more or less close to 3 * this = more than 100;

    // Player Variables
    public float playerSpeed = 12f;
    public float playerBaseSpeed = 12f;

    public float playerFullness = 0f;
    public float maxFullness = 100f;
    public bool hasEaten = false;
    public bool isSick = false;
    public float sickSpeed = 8f;
    public float highScore = 2f;

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
        if (gameTimer > 0)
            gameTimer -= Time.deltaTime;
        else
            gameTimer = 0;

        if (gameTimer <= gameStartTime - followerWaitTime || gameTimer <= 0)
            followPlayer = true;
        

        CheckFullness();
    }

    public void CheckFullness()
    {

        if (hasEaten && !isSick && playerFullness > maxFullness)
        {
            playerSpeed = sickSpeed;
            isSick = true;
        }
        else if (playerFullness <= 34)
        {
            playerSpeed = playerBaseSpeed + (1 * candySpeedBoost);
        }
        else if (playerFullness > 34)
        {
            playerSpeed = playerBaseSpeed + (2 * candySpeedBoost);
        }

        if (playerFullness == 0)
        {
            playerSpeed = playerBaseSpeed;
            isSick = false;
        }

        if (playerFullness > 0)
            playerFullness -= 6 * Time.deltaTime;
        if (playerFullness < 0)
            playerFullness = 0;

        if (isSick)
            playerSpeed = sickSpeed;
    }
    //get candies
    public void GetCandy(int amount)
    {
        candy += amount;
    }

    public void EatCandy()
    {
        if (playerFullness < 100 && !isSick && candy > 0)
        {
            playerFullness += candySaturation;
            candy--;
            hasEaten = true;
        }
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
