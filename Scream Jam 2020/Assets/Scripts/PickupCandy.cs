using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupCandy : MonoBehaviour
{
    public int value = 3;
    void OnTriggerEnter(Collider collisionInfo)
    {
        if (collisionInfo.tag == "Player")
        {
            //add candy
            GameObject.Find("GameController").GetComponent<GameController>().GetCandy(value);
            Destroy(gameObject);
        }
    }
}
