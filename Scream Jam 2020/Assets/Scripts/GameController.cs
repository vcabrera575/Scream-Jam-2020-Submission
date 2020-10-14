using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int candy = 0;
    public float gameTimer = 100f;
    public string message = "";

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
}
