using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform player;
    public bool hasBeenKnocked = false;

    public GameObject candy;
    public int candies = 3;

    // Cooldowns for knocking
    public float knockTimer = 0f; // Time since last knock
    public float knockCooldown = 5f; // Time in seconds 
    public float openTimer = 0f; // Time since door opened
    public float openCooldown = 10f; // Time in seconds


    public AudioSource doorSoundSource;
    public AudioClip doorSound;
    public float volume = 1f;

    public Light doorLightOne;
    public Light doorLightTwo;


    // Start is called before the first frame update
    void Interact()
    {
        int randNumber = Random.Range(1, 5);

        if (knockTimer <= 0)
        {
            doorSoundSource.PlayOneShot(doorSound, volume);
            if (randNumber < 4 && openTimer <= 0 && !hasBeenKnocked)
            {
                hasBeenKnocked = true;
                MakeCandy();
                doorLightOne.enabled = false;
                doorLightTwo.enabled = false;
                openTimer = knockCooldown;
            }
            knockTimer = knockCooldown;
        }

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

    void Update()
    {
        //count down knock timer
        knockTimer -= Time.deltaTime;
        openTimer -= Time.deltaTime;

        if (openTimer <= 0 && hasBeenKnocked)
        {
            hasBeenKnocked = false;
            doorLightOne.enabled = true;
            doorLightTwo.enabled = true;
        }

    }
}
