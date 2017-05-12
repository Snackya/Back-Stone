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


	// Use this for initialization
	void Start () {
        room = GetComponent<BoxCollider2D>();
        roomBounds = room.bounds;
        gate = transform.Find("Gate").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        if (playersInside)
        {
            if (!roomBounds.Contains(player1.position) &&
            !roomBounds.Contains(player2.position))
            {
                gate.SetActive(true);
                playersInside = false;
            }
        }
	}
}
