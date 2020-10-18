using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyController : MonoBehaviour
{

    public GameController gameController;
    public int firstCandyTier = 12;
    public int secondCandyTier = 21;

    // Update is called once per frame
    void Update()
    {

        gameController.CheckFullness();

        if (gameController.candy > firstCandyTier)
        {
            gameController.playerSpeed -= 1f;
        }
        if (gameController.candy > secondCandyTier)
        {
            gameController.playerSpeed -= 1f;
        }
    }

}
