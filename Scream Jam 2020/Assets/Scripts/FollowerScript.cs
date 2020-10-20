using UnityEngine;
using UnityEngine.AI;

public class FollowerScript : MonoBehaviour
{
    public GameController gameController;
    public NavMeshAgent agent;
    public Transform player;
    public GameObject[] waypoints;
    public GameObject destination;
    Vector3 playerPos;

    public AudioSource audio;
    public AudioClip wanderSound;
    public AudioClip chaseSound;
    public AudioClip searchSound;

    // Player Audio Source for Music toggle
    public AudioSource playerAudio;
    public AudioClip regularSong;
    public AudioClip chasingSong;

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
        //get audio source
        audio = GetComponent<AudioSource>();

        // get player's audio source
        playerAudio = player.GetComponent<AudioSource>();

        // Set initial state at spawn
        chaseState = Chase.Wandering;

        //get initial player position
        playerPos = player.position;

        //get array of waypoints
        waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
    }
    void FollowPlayer()
    {
        if (!audio.isPlaying)
        {
            audio.PlayOneShot(chaseSound);
        }
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
                    //sound
                    audio.PlayOneShot(searchSound, 0.7f);
                }
            }
        }
        //failed to find, so start wandering
        if (searchTime <= 0)
        {
            chaseState = Chase.Wandering;
            Debug.Log("Wandering");
            //sound
            audio.Stop();
            audio.PlayOneShot(wanderSound, 0.7f);

            // Change the music
            ChangeMusic();
        }
    }

    void Wandering()
    {
        if (!audio.isPlaying)
        {
            audio.PlayOneShot(wanderSound, 1f);
        }
        //if all waypoints are visited reset them
        int visitedCount = 0;
        for(int i = 0; i<waypoints.Length; i++)
        {
            if (waypoints[i].GetComponent<WaypointScript>().BeenTo == true)
                visitedCount++;
        }
        if (visitedCount == waypoints.Length)
        {
            for (int i = 0; i < waypoints.Length; i++)
            {
                waypoints[i].GetComponent<WaypointScript>().BeenTo = false;
            }
        }
        //search for closest waypoint
        //TODO: fix that it doesnt currently find the closest waypoint sometimes
        if (destination == null || destination.GetComponent<WaypointScript>().BeenTo == true)
        {
            float dist = Vector3.Distance(waypoints[0].transform.position, transform.position);
            for (int i = 0; i < waypoints.Length; i++)
            {
                float tempDist = Vector3.Distance(waypoints[i].transform.position, transform.position);
                //make sure it's not already visited
                if (waypoints[i].GetComponent<WaypointScript>().BeenTo == false)
                {
                    destination = waypoints[i];
                }
                if ((tempDist <= dist) && waypoints[i].GetComponent<WaypointScript>().BeenTo == false)
                {
                    dist = tempDist;
                    destination = waypoints[i];
                }
            }
        }
        //go towards waypoint
        agent.SetDestination(destination.transform.position);
        //when close to it, set its status to visited
        float mdist = Vector3.Distance(destination.transform.position, transform.position);
        if (mdist < 3f)
        {
            destination.GetComponent<WaypointScript>().BeenTo = true;
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
        //wandering
        if (chaseState == Chase.Wandering && gameController.followPlayer)
            Wandering();
        //found player so start chasing
        Vector3 dir = player.position - transform.position;
        RaycastHit hit;
        Debug.DrawRay(transform.position, dir, Color.white, 0.1f, false);
        if (Physics.Raycast(transform.position, dir, out hit) && chaseState != Chase.Chasing)
        {
            if (hit.transform.tag == "Player" && InLineOfSight())
            {
                chaseState = Chase.Chasing;
                //sound
                audio.Stop();
                audio.PlayOneShot(searchSound, 0.7f);

                // Change the music
                ChangeMusic();
            }

        }

    }

    //see if player is in line of sight
    bool InLineOfSight()
    {
        Vector3 targetDir = player.position - transform.position;
        float angle = Vector3.Angle(targetDir, transform.forward);
        float dis = Vector3.Distance(player.position, transform.position);

        return (angle < 90f) && (dis<40f);
    }

    void ChangeMusic()
    {
        //int currentTime = playerAudio.timeSamples;
        //Debug.Log("Current state: " + chaseState);
        //if (chaseState == Chase.Chasing)
        //{
        //    playerAudio.clip = chasingSong;
        //    playerAudio.timeSamples = currentTime;
        //    playerAudio.Play();
        //}
        //else
        //{
        //    playerAudio.clip = regularSong;
        //    playerAudio.timeSamples = currentTime;
        //    playerAudio.Play();
        //}
    }

}