using UnityEngine;
using UnityEngine.AI;

public class FollowerScript : MonoBehaviour
{
    public GameController gameController;
    public NavMeshAgent agent;
    public Transform player;
    Vector3 playerPos;

    public float minDistance = 5f;
    public float maxDistance = 5f;

    enum Chase { Chasing, Wandering};
    Chase chaseState = Chase.Chasing; //0 = chasing player, 1 = wandering

    private void Start()
    {
        playerPos = player.position;
    }
    void FollowPlayer()
    {

        transform.LookAt(player.position);
        agent.speed = gameController.followerSpeed;

        Vector3 dir = player.position - transform.position;
        RaycastHit hit;
        //near position
        if (Vector3.Distance(transform.position, playerPos) <= maxDistance)
        {
            // Player is caught!
            gameController.Caught();
            //player isn't in sight so wander
            if (Physics.Raycast(transform.position, dir, out hit))
            {
                if (hit.transform.tag != "Player")
                {
                    chaseState = Chase.Wandering;
                }
            }
        }
        //update player position if in line of sight
        if (Physics.Raycast(transform.position, dir, out hit))
        {
            if (hit.transform.tag == "Player")
            {
                playerPos = player.position;
            }
            
        }
        //actually move
        agent.SetDestination(playerPos);
    }
    // Update is called once per frame
    void Update()
    {
        //times up so always follow player
        if (gameController.gameTimer < 0)
        {
            chaseState = Chase.Chasing;
            playerPos = player.position;
        }
        //chasing player
        if (chaseState == Chase.Chasing && gameController.followPlayer)
            FollowPlayer();

        //found player so start chasing
        Vector3 dir = player.position - transform.position;
        RaycastHit hit;
        Debug.DrawRay(transform.position, dir, Color.white, 0.1f, false);
        if (Physics.Raycast(transform.position, dir, out hit))
        {
            if (hit.transform.tag == "Player")
            {
                chaseState = Chase.Chasing;
            }

        }
    }

}