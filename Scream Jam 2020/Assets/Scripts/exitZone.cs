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
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 origin = playerCamera.transform.position;
        Vector3 direction = playerCamera.transform.forward;
        RaycastHit hit;

        if (Input.GetButtonDown("Fire1") && Physics.Raycast(origin, direction, out hit, objectDistance))
        {
            // DO something!
            if (hit.transform.tag == "ExitZone")
            {
                if (gameManager.gameTimer >= 0)
                    gameManager.SetMessage("Are you sure you want to exit early?");
                else
                    gameManager.SetMessage("You're too late!");
            }
        }
    }
}
