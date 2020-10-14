using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exitZone : MonoBehaviour
{
    public GameController gameManager;
    public Camera playerCamera;

    public float objectDistance = 2f; // Set distance for how far away the player is from interactable object

    // Start is called before the first frame update
    void Interact()
    {
        if (gameManager.gameTimer >= 0)
            gameManager.SetMessage("Are you sure you want to exit early?");
        else
            gameManager.SetMessage("You're too late!");
    }

}
