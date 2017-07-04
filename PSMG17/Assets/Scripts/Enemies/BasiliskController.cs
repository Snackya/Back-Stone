using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasiliskController : MonoBehaviour {

    public Transform[] targets;
    public GameObject[] screams;
    public Transform screamPosition;
    public float screamSpeed;

    private GameObject scream;
    private Transform target;
    private Rigidbody2D enemy;
    private Animator animator;
    private Renderer enemySprite;

    private float aggroTime = 4f;
    private float timeBetweenAttackChecks = 2f;
    private float headbuttRange = 2.3f;
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
        screamSpeed = 0.01f;
    }

    void OnEnable()
    {
        StartCoroutine(Attack());
        StartCoroutine(SelectNearestTarget());
    }

    // Update is called once per frame
    void Update()
    {

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
        float attackDie = Random.Range(0, rngRange);
        //Debug.Log(Vector2.Distance(enemy.position, target.position));
        //Debug.Log("rolled: " + attackDie);

        if (Vector2.Distance(enemy.position, target.position) < headbuttRange && attackDie < 1f)
        {
            animator.SetTrigger("headbuttTrigger");
        }
        else if(attackDie < 1f)
        {
            animator.SetTrigger("rangedAttackTrigger");
            yield return new WaitForSeconds(0.75f);
            SpawnScream();
        }

        yield return new WaitForSeconds(timeBetweenAttackChecks);
        StartCoroutine(Attack());
    }

    void SpawnScream()
    {
        int random = Random.Range(0, 2);
        scream = screams[random];

        //rotation missing
        GameObject newScream = Instantiate(scream, screamPosition);      
    }
}
