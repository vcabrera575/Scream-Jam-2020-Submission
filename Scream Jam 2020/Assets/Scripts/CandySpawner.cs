using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandySpawner : MonoBehaviour
{
    public Rigidbody candy;
    int candyScore;
    void Start()
    {
        candyScore = PlayerPrefs.GetInt("score", 0);
    }

    // Update is called once per frame
    void Update()
    {
        while (candyScore > 0)
        {
            Rigidbody c = Instantiate(candy, transform.position, transform.rotation);
            candyScore--;
        }
    }
}
