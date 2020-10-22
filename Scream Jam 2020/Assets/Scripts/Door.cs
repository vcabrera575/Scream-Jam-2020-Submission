using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameController gameController;
    public Transform player;
    public GameObject doorHinge;

    public bool hasBeenKnocked = false;
    bool doorIsOpen = false;

    public GameObject candy;
    public int candies = 3;

    // Cooldowns for knocking
    public float knockTimer = 0f; // Time since last knock
    public float openTimer = 0f; // Time since door opened


    public AudioSource doorSoundSource;
    public AudioClip doorSound;
    public float volume = 1f;

    public Light doorLightOne;
    public Light doorLightTwo;


    // Start is called before the first frame update
    void Interact()
    {
        int randNumber = Random.Range(1, 4); // set to 5

        if (knockTimer <= 0 && openTimer <= 0)
        {
            doorSoundSource.PlayOneShot(doorSound, volume);
            if (randNumber < 4 && openTimer <= 0 && !hasBeenKnocked)
            {
                MakeCandy();
                ToggleDoor(true);
                hasBeenKnocked = true;
                doorLightOne.enabled = false;
                doorLightTwo.enabled = false;
                openTimer = gameController.openCooldown;
            }
            knockTimer = gameController.knockCooldown;
        }
        //alert follower to your location
        GameObject.FindGameObjectWithTag("Enemy").GetComponent<FollowerScript>().FindPlayer();
    }

    //makes candy and throws it at the player
    void MakeCandy()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        for(int i = 0; i<candies; i++)
        {
            Transform cm = Instantiate(candy.transform, (player.transform.position + (transform.forward*1.5f)),transform.rotation);
            cm.position = new Vector3(cm.position.x, cm.position.y + 3, cm.position.z);
            cm.position += Random.insideUnitSphere * 0.5f;
        }
    }

    // Switch between opening and closing the door
    void ToggleDoor(bool open)
    {
        if (open)
        {
            doorIsOpen = true;
            // Create a quaternion to move the rotation the direction we need
            Quaternion rotation = doorHinge.transform.rotation * Quaternion.Euler(0, -90, 0);
            doorHinge.transform.rotation = Quaternion.Slerp(doorHinge.transform.rotation, rotation, 5f);
        }
        if (!open)
        {
            doorIsOpen = false;
            Quaternion rotation = doorHinge.transform.rotation * Quaternion.Euler(0, 90, 0);
            doorHinge.transform.rotation = Quaternion.Slerp(doorHinge.transform.rotation, rotation, 5f);
        }

    }

    void Update()
    {
        //count down knock timer
        knockTimer -= Time.deltaTime;
        openTimer -= Time.deltaTime;

        if (hasBeenKnocked && knockTimer <= 0)
        {
            ToggleDoor(false);
            hasBeenKnocked = false;
        }
        if (openTimer <= 0 && !doorIsOpen)
        {
            doorLightOne.enabled = true;
            doorLightTwo.enabled = true;
        }

    }
}
