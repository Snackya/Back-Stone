using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

[RequireComponent (typeof(Rigidbody2D))]
[RequireComponent(typeof(Seeker))]
public class EnemyAI : MonoBehaviour {

    public Transform[] targets;
    private Transform target;                    // target to be chased, e.g. player
    public float speed = 100f;                  // enemy speed
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

    //private bool playerHitted = false;          // checks if the player was currently hitted


    void Start()
    {
        // initializing target as first target at Start()
        target = targets[0];
        seeker = GetComponent<Seeker>();
        enemy = GetComponent<Rigidbody2D>();

        attackSpeed = speed + 50f;

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

        chasingPos -= walkingDirection;
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
        StartCoroutine(CalculateAttackSpeed());
        //StartCoroutine(BackUp());

        // move the enemy
        //enemy.AddForce(waypointDirection, fMode);
        enemy.velocity = waypointDirection;

        if (Vector3.Distance(transform.position, path.vectorPath[currentWayPoint]) < nextWaypointDistance)
        {
            currentWayPoint++;
            return;
        }
    }

    /*private IEnumerator BackUp()
    {
        if (Vector2.Distance(enemy.position, target.position) < 0.6f && playerHitted)
        {
            waypointDirection *= attackSpeed * Time.fixedDeltaTime * (-1);
            //enemy.velocity = waypointDirection * (-1) * Time.fixedDeltaTime;
            playerHitted = false;
        }
        yield return new WaitForSeconds(5f);
        StartCoroutine(BackUp());
    }*/

    private IEnumerator CalculateAttackSpeed()
    {
        if (Vector2.Distance(enemy.position, target.position) > 2)
        {
            waypointDirection *= speed * Time.fixedDeltaTime;
        }
        else
        {
            waypointDirection *= attackSpeed * Time.fixedDeltaTime;
        }

        yield return new WaitForSeconds(5f);
        StartCoroutine(CalculateAttackSpeed());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            enemy.AddForce(waypointDirection * (-800), fMode);
            StartCoroutine(DecreaseVelocity());
            //playerHitted = true;
        }
    }

    // TODO: Magic numbers ändern
    private IEnumerator DecreaseVelocity()
    {
        speed = 10;
        attackSpeed = 15;
        for (int i = 0; i < 8; i++)
        {
            yield return new WaitForSeconds(0.15f);
            speed += 10;
            attackSpeed += 15;
        }
    }

    /*private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && playerHitted)
        {
            Debug.Log("Penis");
            //enemy.AddForce(waypointDirection * (-500), fMode);
            playerHitted = false;
        }
    }*/
}
