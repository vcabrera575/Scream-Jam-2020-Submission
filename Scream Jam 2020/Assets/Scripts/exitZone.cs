using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exitZone : MonoBehaviour
{
    public GameController gameManager;

    // Start is called before the first frame update
    void Interact()
    {
        if (gameManager.gameTimer >= 0)
        {
            gameManager.SetMessage("Are you sure you want to exit early?");
        }
        else
        {
            gameManager.SetMessage("You're too late!");
        }

        if (gameManager.candy > 0)
        {
            gameManager.highScore += gameManager.candy;
            gameManager.candy = 0;
            gameManager.SetMessage("Candy dumped!");
        }
    }

}
