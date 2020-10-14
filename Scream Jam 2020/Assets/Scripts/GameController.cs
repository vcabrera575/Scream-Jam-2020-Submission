using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public int candy = 0;
    public float gameTimer = 100f;
    public string message = "";
    public Text screenMessage;

    // Game states
    bool inProgress = true;
    bool gameEnded = false;
    bool timerEnded = false;

    void Star()
    {
        inProgress = true;
        gameEnded = false;
        timerEnded = false;
    }

    // Update is called once per frame
    void Update()
    {
        //count down timer
        gameTimer -= Time.deltaTime;
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
}
