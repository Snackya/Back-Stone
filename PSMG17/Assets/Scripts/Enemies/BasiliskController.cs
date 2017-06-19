using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasiliskController : MonoBehaviour {

    public Transform[] targets;
    public Transform target;

    private Rigidbody2D enemy;
    private Animator animator;
    private bool facingRight = true;
    private Renderer enemySprite;
    private float aggroTime = 4f;
    private float timeBetweenAttackChecks = 1f;
    private float headbuttRange = 1f;

    void Start()
    {
        enemy = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        enemySprite = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        SelectNearestTarget();

        if (Vector3.Distance(enemy.position, target.position) < 1.5f)
        {
            animator.SetTrigger("Attack");
        }
        else
        {
            animator.SetTrigger("Idle");
        }
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

    void CheckDistanceToTarget()
    {

    }

    IEnumerator Attack()
    {
        float attackDie = Random.Range(0, 10);


        yield return new WaitForSeconds(timeBetweenAttackChecks);
        StartCoroutine(Attack());
    }
}
