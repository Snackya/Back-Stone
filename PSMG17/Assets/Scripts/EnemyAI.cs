using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[RequireComponent (typeof(Rigidbody2D))]
[RequireComponent(typeof(Seeker))]
public class EnemyAI : MonoBehaviour {

    public Transform[] targets;
    private Transform target;                    // target to be chased, e.g. player
    public float speed = 300f;                  // enemy speed
    private float attackSpeed;
    public ForceMode2D fMode;                   // force, that moves the enemy
    public float nextWaypointDistance = 1f;     // max distance from enemy to waypoint, before selecting next waypoint
    public float aggroTime = 4f;

    public Path path;                           // stores the calculated path
    [HideInInspector]
    public bool pathHasEnded = false;

    private float updateRate = 2f;               // determines how often per second the path is updated
    private Seeker seeker;                      
    private Rigidbody2D enemy; 
    private int currentWayPoint = 0;            // currently selected waypoint
    private Vector3 waypointDirection;          // direction to next waypoint


    void Start()
    {
        // initializing target as first target at Start()
        target = targets[0];
        seeker = GetComponent<Seeker>();
        enemy = GetComponent<Rigidbody2D>();

        attackSpeed = speed + 500f;

        if (target == null)
        {
            return;
        }
    
        //seeker.StartPath(transform.position, target.position, OnPathComplete);
        StartCoroutine(UpdatePath());
        StartCoroutine(SelectNearestTarget());
    }

    IEnumerator UpdatePath()
    {
        if (target == null)
        {
            yield return false;
        }

        // create new path from self to target and return the result to 'OnPathComplete'
        seeker.StartPath(transform.position, target.position, OnPathComplete);

        // call self after updateRate time expired
        yield return new WaitForSeconds(1f / updateRate);
        StartCoroutine(UpdatePath());
    }

    IEnumerator SelectNearestTarget()
    {
        float distanceToCurrentTarget = Vector2.Distance(enemy.position, target.position);

        for (int i = 0; i < targets.Length; i++)
        {
            if (!targets[0].gameObject.activeSelf)
            {
                target = targets[1];
            }
            else if (!targets[1].gameObject.activeSelf)
            {
                target = targets[0];
            }
            else if (distanceToCurrentTarget > Vector2.Distance(enemy.position, targets[i].position))
            {
                target = targets[i];
            }
        }
            
        yield return new WaitForSeconds(aggroTime);
        StartCoroutine(SelectNearestTarget());
    }

    public void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            // setting the new path, if possible
            path = p;
            currentWayPoint = 0;
        }
    }

    void FixedUpdate()
    {
        if (target == null) return;
        if (path == null) return;

        if (currentWayPoint >= path.vectorPath.Count)
        {
            if (pathHasEnded) return;
            
            pathHasEnded = true;
            return;
        }
        pathHasEnded = false;

        waypointDirection = (path.vectorPath[currentWayPoint] - transform.position).normalized;

        if (Vector2.Distance(enemy.position, target.position) > 2)
        {
            waypointDirection *= speed * Time.fixedDeltaTime;
        }
        else
        {
            waypointDirection *= attackSpeed * Time.fixedDeltaTime;
        }

        // move the enemy
        enemy.AddForce(waypointDirection, fMode);

        if (Vector3.Distance(transform.position, path.vectorPath[currentWayPoint]) < nextWaypointDistance)
        {
            currentWayPoint++;
            return;
        }
    }
}
