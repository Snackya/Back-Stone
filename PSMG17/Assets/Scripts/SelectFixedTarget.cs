using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectFixedTarget : MonoBehaviour {

    [SerializeField]
    private Transform player1;
    [SerializeField]
    private Transform player2;

    private Bounds trapRoomBounds;
    private int playerNumber = 0;
	// Use this for initialization
	void Start () {
        trapRoomBounds = GetComponent<BoxCollider2D>().bounds;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (trapRoomBounds.Contains(player1.position)) playerNumber = 0;
        else playerNumber = 1;

		foreach (Transform enemy in transform)
        {

            EnemyAI enemyAI = enemy.GetComponent<EnemyAI>();
            enemyAI.fixedTarget = true;
            enemyAI.fixedPlayerNumber = playerNumber;
        }
	}
}
