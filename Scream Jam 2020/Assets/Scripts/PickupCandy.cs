using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupCandy : MonoBehaviour
{
    public int value = 3;
    public GameObject chocolateBar;
    public GameObject candyCorn;
    public GameObject lollipop;

    void Start()
    {
        int randomNum = Random.Range(1, 4);

        switch (randomNum)
        {
            case 1:
                chocolateBar.SetActive(true);
                break;
            case 2:
                candyCorn.SetActive(true);
                break;
            case 3:
                lollipop.SetActive(true);
                break;
            default:
                chocolateBar.SetActive(true);
                break;
        }

    }

    void Update()
    {
        Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        if (Vector3.Distance(playerPosition, transform.position) <= 2f)
        {
            GameObject.Find("GameController").GetComponent<GameController>().GetCandy(value);
            Destroy(gameObject);
            // make sure the candy bucket isn't full
            //NOTE: I think letting the player have unlimited candy in bucket is okay since he has a speed penalty so i commented this out for now
            //if (!GameObject.Find("GameController").GetComponent<GameController>().bucketIsFull)
            //{
            //    GameObject.Find("GameController").GetComponent<GameController>().GetCandy(value);
            //    Destroy(gameObject);
            //}
            //else
            //    GameObject.Find("GameController").GetComponent<GameController>().SetMessage("Your bucket is full!");
        }
    }
}
