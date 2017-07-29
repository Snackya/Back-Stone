using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherAI : MonoBehaviour {

    private Transform target;
    private Rigidbody2D enemy;

	// Use this for initialization
	void OnEnable () {
        target = GetComponent<EnemyAI>().target;
        enemy = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        target = GetComponent<EnemyAI>().target;

        StopAtShootingRange();
    }

    private void StopAtShootingRange()
    {
        if (Vector2.Distance(transform.position, target.position) < 4)
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
}
