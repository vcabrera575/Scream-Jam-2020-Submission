using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool hasBeenKnocked = false;
    public float volume = 1f;
    public int candies = 3;

    public AudioSource doorSoundSource;
    public AudioClip doorSound;
    public Light doorLightOne;
    public Light doorLightTwo;


    // Start is called before the first frame update
    void Interact()
    {
        if (!hasBeenKnocked)
        {
            hasBeenKnocked = true;
            doorSoundSource.PlayOneShot(doorSound, volume);
            MakeCandy();
            doorLightOne.enabled = false;
            doorLightTwo.enabled = false;
        }
    }
    //makes candy and throws it at the player
    void MakeCandy()
    {
        for(int i = 0; i<candies; i++)
        {
            Transform c = GameObject.Find("Candy").transform;
            Transform cm = Instantiate(c, transform.position + (transform.forward*1.5f),transform.rotation);
            cm.position = new Vector3(cm.position.x, cm.position.y + 3, cm.position.z);
            cm.position += Random.insideUnitSphere * 0.5f;
        }
    }

}
