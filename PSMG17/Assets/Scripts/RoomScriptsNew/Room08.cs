using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room08 : MonoBehaviour {

    [SerializeField]
    private Transform player1;
    [SerializeField]
    private Transform player2;

    private Bounds roomBounds;

    private bool playersInside = false;

    private Beehive beehive;


    void Start () {
        roomBounds = transform.GetComponent<BoxCollider2D>().bounds;
        beehive = GetComponentInChildren<Beehive>();
	}
	
	void Update () {
        ActivateBeehive();
	}

    private void ActivateBeehive()
    {
        if (roomBounds.Contains(player1.position) && roomBounds.Contains(player2.position))
        {
            if (!playersInside)
            {
                playersInside = true;
                beehive.SpawnBees();
            }
        }
    }

    public void resetRoom()
    {
        playersInside = false;
    }
}
