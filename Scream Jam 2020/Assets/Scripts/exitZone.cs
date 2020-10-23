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
        /* With the new implementation of the exit arrow, we do not need this anymore.
        if (gameManager.gameTimer >= 0)
        {
            gameManager.SetMessage("Are you sure you want to exit early?");
        }
        else
        {
            gameManager.SetMessage("You're late, Lets go!!");
        }
        */

        if (gameManager.candy > 0)
        {
            gameManager.highScore += gameManager.candy;
            gameManager.candy = 0;
            gameManager.SetMessage("Candy dumped!");
        }
    }

}
