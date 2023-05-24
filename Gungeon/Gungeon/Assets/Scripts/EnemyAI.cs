/*
 * Customized version of the AI script associated with A* algorithm
 * which sets up waypoints, paths, calculates target position
 * and creates shortest path to move enemy toward player, recalculating
 * at each waypoint.  Much of this was garnished from two differing tutorials
 * by Brackey's, but tweaked for this game's purpose.
 * 
 * by Wendy Slattery, Nov 2019
 */

using Pathfinding;
using System.Collections;
using UnityEngine;


[RequireComponent(typeof(Seeker))]
public class EnemyAI : MonoBehaviour
{
    public Transform targetPlayer;
    public float updateRatePerSec = 2f;
    private Seeker seeker;
    public Path pathFollowing;
    public float speed = 1f;
    public bool pathCompleted = false;
    public float nextWaypointDistance = 3f;
    private int currentWaypoint = 0;
    private bool searchingForPlayer = false;

    private void Start()
    {
        seeker = GetComponent<Seeker>();

        if (targetPlayer == null)
        {
            if (!searchingForPlayer)
            {
                searchingForPlayer = true;
                StartCoroutine(FindPlayer());
            }
            return;
        }
        StartCoroutine(UpdatePath());

        seeker.StartPath(transform.position, targetPlayer.position, OnPathComplete);
    }

    private IEnumerator FindPlayer()
    {
        GameObject searchResult = GameObject.FindGameObjectWithTag("Player");
        if (searchResult == null)
        {
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(FindPlayer());
        }
        else
        {
            targetPlayer = searchResult.transform;
            searchingForPlayer = false;
            StartCoroutine(UpdatePath());
        }
    }

    private IEnumerator UpdatePath()
    {
        if (targetPlayer == null)
        {
            if (!searchingForPlayer)
            {
                searchingForPlayer = true;
                StartCoroutine(FindPlayer());
            }
            yield return false;
        }
        else
        {
            seeker.StartPath(transform.position, targetPlayer.position, OnPathComplete);
            yield return new WaitForSeconds(1f / updateRatePerSec);
            StartCoroutine(UpdatePath());
        }
    }
    public void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            pathFollowing = p;
            currentWaypoint = 0;
        }
    }
    private void FixedUpdate()
    {
        if (targetPlayer == null)
        {
            if (!searchingForPlayer)
            {
                searchingForPlayer = true;
                StartCoroutine(FindPlayer());
            }
            return;
        }
        
        // make sure enemy is always facing player
        if (transform.position.x > targetPlayer.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        
        if (pathFollowing == null)
        {
            return;
        }

        if (currentWaypoint >= pathFollowing.vectorPath.Count)
        {
            if (pathCompleted)
            {
                return;
            }
            
            StartCoroutine(FindPlayer());  // set new position for player at end
            pathCompleted = true;
            return;
        }
        pathCompleted = false;

        float distanceToWaypoint;
        while (true)
        {
            distanceToWaypoint = Vector3.Distance(transform.position, pathFollowing.vectorPath[currentWaypoint]);
            if (distanceToWaypoint < nextWaypointDistance)
            {
                // search if another waypoint or if path complete
                if (currentWaypoint + 1 < pathFollowing.vectorPath.Count)
                {
                    currentWaypoint++;
                }
                else
                {
                    pathCompleted = true;
                    break;
                }
            }
            else
            {
                break;
            }
        }
        
        var speedFactor = pathCompleted ? Mathf.Sqrt(distanceToWaypoint / nextWaypointDistance) : 1f;  // transitions ending with slower speed

        Vector3 direction = (pathFollowing.vectorPath[currentWaypoint] - transform.position).normalized;
        Vector3 velocity = direction * speed * speedFactor;

        if (!pathCompleted)
            transform.position += velocity * Time.deltaTime;

    }
}

