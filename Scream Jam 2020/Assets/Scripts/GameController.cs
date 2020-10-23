using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    // Game timer management
    public bool gamePaused = false;

    float gameStartTime = 0f;
    public float gameTimer = 100f;
    public string message = "";
    public bool messageChanged = false;
    public float messageOnScreenTimer = 3f;   // How many seconds will the message be on screen, multiplied by 2 
    public float satiationDecreaseSpeed = 20f; // How many seconds will it take for the player's candy meter go down 
    public float satiationDecreaseSpeedNormal = 20f;
    public float satiationDecreaseSpeedSick = 10f;

    // Follower Variables
    public bool followPlayer = false;
    public bool debugFollowPlayer = true;
    public float followerWaitTime = 15f;
    public float followerSpeed = 12f;
    float followerSpeedBoostTimer = 0f;

    // Candy management
    public int candy = 0;
    public float candySpeedBoost = 2f;
    public float candySaturation = 34f; // A number that is more or less close to 3 * this = more than 100;
    public float candySpeedTimerMax = 3f;
    public float candySpeedBoostFixed = 15f;
    public float candySpeedTimer = 0f;

    // Candy Weight Management tiers
    public bool bucketIsFull = false;
    public int firstCandyTier = 15;
    public int secondCandyTier = 30;
    public int maxCandyInBucket = 20;

    // Door knockers
    public float knockCooldown = 5f; // Time in seconds 
    public float openCooldown = 10f; // Time in seconds

    // Player Variables
    public float playerSpeed = 10f;
    public float playerBaseSpeed = 10f;

    public float playerFullness = 0f;
    public float maxFullness = 100f;
    public bool hasEaten = false;
    public bool isSick = false;
    public float sickSpeed = 8f;
    public int highScore = 0;

    // Game states
    bool inProgress = true;
    bool gameEnded = false;
    bool timerEnded = false;

    // UI selectors
    public Canvas UserInterface;
    public Canvas PauseScreen;
    public Text scoreText;

    void Start()
    {
        gameStartTime = gameTimer;
        inProgress = true;
        gameEnded = false;
        timerEnded = false;
        followPlayer = false;

        // Start the game
        gamePaused = true;
        PauseGame();

        // Hide and lock the cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
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

        //increase follower speed overtime
        followerSpeedBoostTimer += Time.deltaTime;
        if (followerSpeedBoostTimer >= 60)
        {
            followerSpeed += 1f;
            followerSpeedBoostTimer = 0;
        }
    }

    // Pause the game
    public void PauseGame()
    {
        if (!gamePaused)
        {
            // Pause the game
            gamePaused = true;
            inProgress = false;
            Time.timeScale = 0;
            AudioListener.pause = true;

            // Enable the cursor
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            // Canvas disable
            UserInterface.gameObject.SetActive(false);
            PauseScreen.gameObject.SetActive(true);
            scoreText.text = "Current Score: " + (highScore + candy).ToString();
        }
        else
        {
            // Unpause the game
            inProgress = true;
            gamePaused = false;
            Time.timeScale = 1;
            AudioListener.pause = false;

            // Disable the cursor
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;


            // Canvas flip
            UserInterface.gameObject.SetActive(true);
            PauseScreen.gameObject.SetActive(false);
        }
    }

    // Go back to the main menu
    public void QuitGame()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
        SceneManager.LoadScene(0);
    }



    // Check to see how full the player is. 
    // Only referenced from CandyController right now.
    public void CheckFullness()
    {

        // If the player is full but hasn't been sick eyt
        if (hasEaten && !isSick && playerFullness > maxFullness)
        {
            playerFullness = maxFullness;
            playerSpeed = sickSpeed;
            isSick = true;
        }

        if (playerFullness == 0)
        {
            playerSpeed = playerBaseSpeed;
            isSick = false;
        }


        // Make sure that we decrease timer
        if ((playerFullness > 0)
        && ((!isSick) || (isSick && candySpeedTimer<=0))) //this is so the fullness doesn't tick down while the player is sick and benefiting from candy boost
            playerFullness -= Time.deltaTime / (satiationDecreaseSpeed / maxFullness); // Frame time / (Seconds / total we need to get rid of)
        if (playerFullness < 0)
            playerFullness = 0;

        // If the player is sick, make sure none of the changes above happened
        if (isSick)
        {
            playerSpeed = sickSpeed;
            satiationDecreaseSpeed = satiationDecreaseSpeedSick;
            //alert follower to your location
            GameObject.FindGameObjectWithTag("Enemy").GetComponent<FollowerScript>().FindPlayer();
        }
        else
        {
            playerSpeed = playerBaseSpeed;
            satiationDecreaseSpeed = satiationDecreaseSpeedNormal;
        }
        // Candy Basket Weight Calculation
        if (candy > firstCandyTier)
        {
            playerSpeed -= 2f;
        }
        if (candy > secondCandyTier)
        {
            playerSpeed -= 2f;
        }
        //make sure speed doesn't become negative
        playerSpeed = Mathf.Max(playerSpeed, 1f);
        //candy speed boost, overrides all
        if (candySpeedTimer > 0)
        {
            playerSpeed = candySpeedBoostFixed;
            candySpeedTimer -= Time.deltaTime;
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
            candySpeedTimer = candySpeedTimerMax;
        }
    }

    // Set message that appears on screen
    public void SetMessage(string newMessage)
    {
        message = newMessage;
        messageChanged = true;
    }

    // Player is caught
    public void Caught()
    {
        gameEnded = true;

        // Enable the cursor
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        SceneManager.LoadScene(2);
    }
}
