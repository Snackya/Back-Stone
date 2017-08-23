using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasiliskController : MonoBehaviour {

    public Transform[] targets;
    public GameObject[] screams;
    public Transform screamPosition;

    private GameObject scream;
    [HideInInspector] public Transform target;
    private Rigidbody2D enemy;
    private Animator animator;
    private Renderer enemySprite;

    private float aggroTime = 1f;
    private float timeBetweenAttackChecks = 1f;
    private float headbuttRange = 5f;
    private float rngRange = 2f;


    void Awake()
    {
        // initializing target as first target at Awake()
        target = targets[0];
        enemy = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        enemySprite = GetComponent<Renderer>();
    }

    void Start()
    {
    }

    void OnEnable()
    {
        StartCoroutine(Attack());
        StartCoroutine(SelectNearestTarget());
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

    IEnumerator Attack()
    {
        float attackDie = UnityEngine.Random.Range(0, rngRange);

        // if both players are out of the headbutt range, the basilisk spams its range attack
        if (Vector2.Distance(enemy.position, targets[0].position) > headbuttRange &&
            Vector2.Distance(enemy.position, targets[1].position) > headbuttRange)
        {
            animator.SetTrigger("rangedAttackTrigger");
            yield return new WaitForSeconds(0.75f);
            SelectTarget();
            SpawnScream();
        }
        else if (Vector2.Distance(enemy.position, target.position) < headbuttRange && attackDie < 1f)
        {
            animator.SetTrigger("headbuttTrigger");
        }
        else if (attackDie > 1f)
        {
            animator.SetTrigger("rangedAttackTrigger");
            yield return new WaitForSeconds(0.75f);
            SelectTarget();
            SpawnScream();
        }

        yield return new WaitForSeconds(timeBetweenAttackChecks);
        StartCoroutine(Attack());
    }

    private void SelectTarget()
    {
        int randomIndex = UnityEngine.Random.Range(0, 2);
        target = targets[randomIndex];

        // Select other target, if selected target is in headbutt range
        if (Vector2.Distance(enemy.position, target.position) < headbuttRange)
        {
            target = targets[1 - randomIndex];
        }
    }

    void SpawnScream()
    {
        int random = UnityEngine.Random.Range(0, 2);
        scream = screams[random];

        //rotation missing
        GameObject newScream = Instantiate(scream, screamPosition);      
    }
}
