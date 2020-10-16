using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerScript : MonoBehaviour
{
    public GameController gameController;
    public Transform player;

    public float followSpeed = 2f;
    public float minDistance = 5f;
    public float maxDistance = 5f;

    void FollowPlayer()
    {
        transform.LookAt(player.position);

        if (Vector3.Distance(transform.position, player.position) >= minDistance)
        {
            // Move the enemy towards the player.
            transform.position = Vector3.MoveTowards(transform.position, player.position, (gameController.followerSpeed * Time.deltaTime) );
            if (Vector3.Distance(transform.position, player.position) <= maxDistance)
            {
                // Player is caught!
                gameController.Caught();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameController.followPlayer)
            FollowPlayer();
    }

}