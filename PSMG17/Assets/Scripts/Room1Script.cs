using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room1Script : MonoBehaviour {

    [SerializeField]
    private Transform player1;
    [SerializeField]
    private Transform player2;

    private BoxCollider2D room;
    private Bounds roomBounds;
    private GameObject gate;
    private bool playersInside = true;


	void Start () {
        room = GetComponent<BoxCollider2D>();
        roomBounds = room.bounds;
        gate = transform.Find("Gate").gameObject;
	}
	
	void Update () {
        // only execute the following code, if the players are located in this room currently
        if (playersInside)
        {
            // check if the players are currently inside the room and closing the gate, when they
            // are leaving
            if (!roomBounds.Contains(player1.position) && !roomBounds.Contains(player2.position))
            {
                gate.SetActive(true);
                playersInside = false;
            }
        }
	}
}
