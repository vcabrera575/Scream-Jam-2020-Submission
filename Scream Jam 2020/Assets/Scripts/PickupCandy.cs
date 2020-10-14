using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupCandy : MonoBehaviour
{
    public int value = 3;
    void Update()
    {
        Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        if (Vector3.Distance(playerPosition, transform.position) <= 2f)
        {
            GameObject.Find("GameController").GetComponent<GameController>().GetCandy(value);
            Destroy(gameObject);
        }
    }
}
