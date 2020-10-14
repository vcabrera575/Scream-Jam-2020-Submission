using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public Camera playerCamera;
    public float objectDistance = 2f; // Set distance for how far away the player is from interactable object
    float knockTimer = 0f;
    public float knockCooldown = 5f; // Time in seconds


    // Update is called once per frame
    void Update()
    {
        //player clicked, so check what they're pointing at
        if (Input.GetButtonDown("Fire1"))
        {
            Vector3 origin = playerCamera.transform.position;
            Vector3 direction = playerCamera.transform.forward;
            RaycastHit hit;

            if (Physics.Raycast(origin, direction, out hit, objectDistance) && knockTimer <= 0)
            {
                // Player knocked on door
                if (hit.transform.tag == "Door")
                {
                    knockTimer = knockCooldown;
                    hit.collider.gameObject.SendMessage("Interact");
                }

                // Player clicked on the exit
                if (hit.transform.tag == "ExitZone")
                {
                    hit.collider.gameObject.SendMessage("Interact");
                }
            }
        }

        //count down knock timer
        knockTimer -= Time.deltaTime;
    }
}
