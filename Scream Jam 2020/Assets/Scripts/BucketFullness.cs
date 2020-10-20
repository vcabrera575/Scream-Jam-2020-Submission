using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BucketFullness : MonoBehaviour
{
    public GameController gameController;
    public GameObject someCandy;
    public GameObject lotsOfCandy;
    public GameObject fullCandy;

    // Update is called once per frame
    void Update()
    {
        gameController.bucketIsFull = false;
        if (gameController.candy == 0)
        {
            someCandy.SetActive(false);
            lotsOfCandy.SetActive(false);
            fullCandy.SetActive(false);
        }
        if (gameController.candy > 1)
        {
            someCandy.SetActive(true);
            lotsOfCandy.SetActive(false);
            fullCandy.SetActive(false);
        }
        if (gameController.candy >= gameController.firstCandyTier)
        {
            someCandy.SetActive(true);
            lotsOfCandy.SetActive(true);
            fullCandy.SetActive(false);
        }
        if (gameController.candy >= gameController.secondCandyTier)
        {
            someCandy.SetActive(true);
            lotsOfCandy.SetActive(true);
            fullCandy.SetActive(true);
        }
        if (gameController.candy >= gameController.maxCandyInBucket)
            gameController.bucketIsFull = true;
    }
}
