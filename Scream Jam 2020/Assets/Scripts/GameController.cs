﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    // Game timer management
    float gameStartTime = 0f;
    public float gameTimer = 100f;
    public string message = "";
    public bool messageChanged = false;
    public float messageOnScreenTimer = 3f;   // How many seconds will the message be on screen, multiplied by 2 
    public float satiationDecreaseSpeed = 6f; // How many seconds will it take for the player's candy meter go down 

    // Follower Variables
    public bool followPlayer = false;
    public bool debugFollowPlayer = true;
    public float followerWaitTime = 15f;
    public float followerSpeed = 8f;

    // Candy management
    public int candy = 0;
    public float candySpeedBoost = 2f;
    public float candySaturation = 34f; // A number that is more or less close to 3 * this = more than 100;

    // Candy Weight Management tiers
    public bool bucketIsFull = false;
    public int firstCandyTier = 9;
    public int secondCandyTier = 15;
    public int maxCandyInBucket = 20;

    // Door knockers
    public float knockCooldown = 5f; // Time in seconds 
    public float openCooldown = 10f; // Time in seconds

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
        {
            gameTimer = 0;
            timerEnded = true;
        }

        if (gameTimer <= gameStartTime - followerWaitTime || gameTimer <= 0)
            followPlayer = debugFollowPlayer;

        CheckFullness();
    }

    // Check to see how full the player is. 
    // Only referenced from CandyController right now.
    public void CheckFullness()
    {

        // If the player is full but hasn't been sick eyt
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


        // Make sure that we decrease timer
        if (playerFullness > 0)
            playerFullness -= Time.deltaTime / (satiationDecreaseSpeed / maxFullness); // Frame time / (Seconds / total we need to get rid of)
        if (playerFullness < 0)
            playerFullness = 0;

        // If the player is sick, make sure none of the changes above happened
        if (isSick)
            playerSpeed = sickSpeed;

        // Candy Basket Weight Calculation
        if (candy > firstCandyTier)
        {
            playerSpeed -= 1f;
        }
        if (candy > secondCandyTier)
        {
            playerSpeed -= 1f;
        }
    }

    //get candies
    public void GetCandy(int amount)
    {
        candy += amount;
    }
    //get how many candies are in bucket
    public bool GetBucketFullness()
    {
        return bucketIsFull;
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
        messageChanged = true;
    }

    public void Caught()
    {
        gameEnded = true;
    }
}
