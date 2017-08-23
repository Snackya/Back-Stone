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

    private Beehive beehiveScript;
    private Transform beehive;
    private Transform door;


    void Start () {
        roomBounds = transform.GetComponent<BoxCollider2D>().bounds;
        beehiveScript = GetComponentInChildren<Beehive>();
        beehive = transform.FindChild("Beehive");
        door = transform.FindChild("Door");
    }
	
	void Update () {
        ActivateBeehive();
        OpenDoor();
	}

    private void ActivateBeehive()
    {
        if (roomBounds.Contains(player1.position) && roomBounds.Contains(player2.position))
        {
            if (!playersInside)
            {
                playersInside = true;
                beehiveScript.SpawnBees();
            }
        }
    }

    private void OpenDoor()
    {
        if (!beehive.gameObject.activeSelf)
        {
            door.GetChild(0).gameObject.SetActive(true);
            door.GetChild(1).gameObject.SetActive(false);
        }
    }

    public void resetRoom()
    {
        playersInside = false;
        beehiveScript.ResetBeehive();

        door.GetChild(0).gameObject.SetActive(false);
        door.GetChild(1).gameObject.SetActive(true);
    }
}
