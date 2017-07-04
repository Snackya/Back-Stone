using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

[RequireComponent (typeof(Rigidbody2D))]
[RequireComponent(typeof(Seeker))]
public class EnemyAI : MonoBehaviour {

    [SerializeField]
    public Transform[] targets;
    [SerializeField]
    public float speed = 4f;                        // enemy speed
    [SerializeField]
    public float nextWaypointDistance = 1f;         // max distance from enemy to waypoint, before selecting next waypoint
    [SerializeField]
    public float aggroTime = 4f;

    private Transform target;                       // target to be chased, e.g. player
    private float attackSpeed;
    private float maxVelocity = 3f;
    private ForceMode2D fMode = ForceMode2D.Force;  // force, that moves the enemy

    public Path path;                               // stores the calculated path
    [HideInInspector]
    public bool pathHasEnded = false;

    private float updateRate = 2f;                  // determines how often per second the path is updated
    private Seeker seeker;                      
    private Rigidbody2D enemy; 
    private int currentWayPoint = 0;                // currently selected waypoint
    private Vector3 waypointDirection;              // direction to next waypoint

    void Awake() {

        // initializing target as first target at Awake()
        target = targets[0];
    }

    void Start()
    {
        seeker = GetComponent<Seeker>();
        enemy = GetComponent<Rigidbody2D>();

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

        Vector3 chasingPos = SetChasingPosition();

        // create new path from self to target and return the result to 'OnPathComplete'
        seeker.StartPath(transform.position, chasingPos, OnPathComplete);

        // call self after updateRate time expired
        yield return new WaitForSeconds(1f / updateRate);
        StartCoroutine(UpdatePath());
    }

    private Vector3 SetChasingPosition()
    {
        Vector3 chasingPos = target.position;
        Vector3 walkingDirection = (transform.position - target.position).normalized;

        chasingPos -= (walkingDirection / 2);
        return chasingPos;
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

        enemy.AddForce(waypointDirection * speed, fMode);

        if (Vector3.Distance(transform.position, path.vectorPath[currentWayPoint]) < nextWaypointDistance)
        {
            currentWayPoint++;
            return;
        }
    }
   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Knockback();
        }
    }

    public void Knockback()
    {
        enemy.AddForce(waypointDirection * (-150), fMode);
    }
}
