using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasiliskController : MonoBehaviour {

    public Transform[] targets;

    private Transform target;
    private Rigidbody2D enemy;
    private Animator animator;
    private Renderer enemySprite;
    private float aggroTime = 4f;
    private float timeBetweenAttackChecks = 2f;
    private float headbuttRange = 2.3f;
    private float rngRange = 2f;

    void Start()
    {
        enemy = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        enemySprite = GetComponent<Renderer>();

        StartCoroutine(Attack());
        StartCoroutine(SelectNearestTarget());
    }

    void Awake()
    {
        // initializing target as first target at Awake()
        target = targets[0];
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
        Debug.Log(Vector2.Distance(enemy.position, target.position));
        Debug.Log("rolled: " + attackDie);

        if (Vector2.Distance(enemy.position, target.position) < headbuttRange && attackDie < 1f)
        {
            animator.SetTrigger("headbuttTrigger");
        }
        else if(attackDie < 1f)
        {
            animator.SetTrigger("rangedAttackTrigger");
        }

        yield return new WaitForSeconds(timeBetweenAttackChecks);
        StartCoroutine(Attack());
    }
}
