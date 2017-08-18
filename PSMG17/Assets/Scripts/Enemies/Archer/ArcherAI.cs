using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherAI : MonoBehaviour {

    [SerializeField]
    private GameObject arrow;
    [SerializeField]
    private Transform arrowPos;

    [SerializeField]
    private Transform target;
    private Rigidbody2D enemy;

    private float attackRange = 5f;

	// Use this for initialization
	void OnEnable () {
        enemy = GetComponent<Rigidbody2D>();
        StartCoroutine(ShootArrows());
	}


    // Update is called once per frame
    void Update () {
        target = GetComponent<EnemyAI>().target;
        StopAtShootingRange();
    }

    private void StopAtShootingRange()
    {
        if (Vector2.Distance(transform.position, target.position) < attackRange)
        {
            enemy.constraints = RigidbodyConstraints2D.FreezePositionX | 
                RigidbodyConstraints2D.FreezePositionY |
                RigidbodyConstraints2D.FreezeRotation;
        }
        else
        {
            enemy.constraints = RigidbodyConstraints2D.None;
            enemy.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }


    private IEnumerator ShootArrows()
    {
        if (Vector2.Distance(transform.position, target.position) < attackRange)
        {
            GameObject newArrow = Instantiate(arrow, arrowPos);
        }
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(ShootArrows());
    }
}
