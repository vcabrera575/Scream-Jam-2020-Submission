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
    float searchTimeMax = 5f;
    float searchTime = 5f;
    float incrementSearch = 0f; //used to determine when to change direction when searching
    float incrementSearchAmount = 1f;

    enum Chase { Chasing, Wandering, Searching};
    Chase chaseState = Chase.Chasing; //0 = chasing player, 1 = wandering

    private void Start()
    {
        playerPos = player.position;
    }
    void FollowPlayer()
    {
        //transform.LookAt(player.position);
        agent.speed = gameController.followerSpeed;

        Vector3 dir = player.position - transform.position;
        RaycastHit hit;
        //near position
        if (Vector3.Distance(transform.position, playerPos) <= maxDistance)
        {
            //player isn't in sight so search for a little bit
            if (Physics.Raycast(transform.position, dir, out hit))
            {
                if (hit.transform.tag != "Player" || !InLineOfSight())
                {
                    chaseState = Chase.Searching;
                    searchTime = searchTimeMax;
                    incrementSearch = 0f;
                    incrementSearchAmount = 0f;
                    Debug.Log("Searching");
                }
            }
        }
        //update player position if in line of sight
        if (Physics.Raycast(transform.position, dir, out hit))
        {
            if (hit.transform.tag == "Player" && InLineOfSight())
            {
                playerPos = player.position;
            }
            
        }
        //actually move
        agent.SetDestination(playerPos);
    }

    void SearchPlayer()
    {
        searchTime -= Time.deltaTime;
        incrementSearch += Time.deltaTime;
        //every 1 second change look direction
        if (incrementSearchAmount < incrementSearch)
        {
            incrementSearchAmount += 1f;
            transform.Rotate(0f, Random.Range(0.0f, 360.0f), 0f);
        }
        //check if player is in sight
        Vector3 dir = player.position - transform.position;
        RaycastHit hit;
        //near position
        if (Vector3.Distance(transform.position, playerPos) <= maxDistance)
        {
            //player isn't in sight so search for a little bit
            if (Physics.Raycast(transform.position, dir, out hit))
            {
                if (hit.transform.tag == "Player" && InLineOfSight())
                {
                    chaseState = Chase.Chasing;
                    searchTime = searchTimeMax;
                }
            }
        }
        //failed to find, so start wandering
        if (searchTime <= 0)
        {
            chaseState = Chase.Wandering;
            Debug.Log("Wandering");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //times up so always follow player
        if (gameController.gameTimer <= 0)
        {
            chaseState = Chase.Chasing;
            playerPos = player.position;
        }
        //chasing player
        if (chaseState == Chase.Chasing && gameController.followPlayer)
            FollowPlayer();
        //search for player
        if (chaseState == Chase.Searching && gameController.followPlayer)
            SearchPlayer();
        //found player so start chasing
        Vector3 dir = player.position - transform.position;
        RaycastHit hit;
        Debug.DrawRay(transform.position, dir, Color.white, 0.1f, false);
        if (Physics.Raycast(transform.position, dir, out hit))
        {
            if (hit.transform.tag == "Player" && InLineOfSight())
            {
                chaseState = Chase.Chasing;
            }

        }
    }

    //see if player is in line of sight
    bool InLineOfSight()
    {
        Vector3 targetDir = player.position - transform.position;
        float angle = Vector3.Angle(targetDir, transform.forward);

        return (angle < 90f);
    }

}