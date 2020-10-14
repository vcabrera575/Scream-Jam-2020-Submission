using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool hasBeenKnocked = false;
    public float knockCooldown = 5f; // Time in seconds
    float knockTimer = 0f;
    public float objectDistance = 2f; // Set distance for how far away the player is from interactable object
    public float volume = 1f;

    public Camera playerCamera;
    public AudioSource doorSoundSource;
    public AudioClip doorSound;


    // Start is called before the first frame update
    void Interact()
    {
        hasBeenKnocked = true;
        doorSoundSource.PlayOneShot(doorSound, volume);
    }

    void Update()
    {
        //player knocks, so give him stuff
        if (Input.GetButtonDown("Fire1"))
        {
            Vector3 origin = playerCamera.transform.position;
            Vector3 direction = playerCamera.transform.forward;
            RaycastHit hit;

            if (Physics.Raycast(origin, direction, out hit, objectDistance) && knockTimer<=0)
            {
                if (hit.transform.tag == "Door")
                {
                    knockTimer = knockCooldown;
                    Interact();
                }
            }
        }

        //count down knock timer
        knockTimer -= Time.deltaTime;
    }
}
