using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public Camera playerCamera;
    public GameController gameController;
    public float objectDistance = 2f; // Set distance for how far away the player is from interactable object


    // Update is called once per frame
    void Update()
    {
        //player clicked, so check what they're pointing at
        if (Input.GetButtonDown("Fire1"))
        {
            Vector3 origin = playerCamera.transform.position;
            Vector3 direction = playerCamera.transform.forward;
            RaycastHit hit;

            if (Physics.Raycast(origin, direction, out hit, objectDistance))
            {

                // Player knocked on door
                if (hit.transform.tag == "Door")
                {
                    hit.collider.gameObject.SendMessage("Interact");
                }

                // Player clicked on the exit
                if (hit.transform.tag == "ExitZone")
                {
                    hit.collider.gameObject.SendMessage("Interact");
                }
            }
        }

        // If player presses 
        if (Input.GetButtonDown("Fire2"))
        {
            gameController.EatCandy();
        }

    }
}
