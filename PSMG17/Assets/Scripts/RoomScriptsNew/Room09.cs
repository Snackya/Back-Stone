using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room09 : MonoBehaviour {

    [SerializeField]
    private Transform player1;
    [SerializeField]
    private Transform player2;
    [SerializeField]
    private Beehive[] beehiveScripts;
    [SerializeField]
    private Transform[] beehives;
    private Bounds roomBounds;
    private bool playersInside = false;
    private Transform door;

    // Use this for initialization
    void Start () {
        roomBounds = transform.GetComponent<BoxCollider2D>().bounds;
        door = transform.FindChild("Door");
    }

    // Update is called once per frame
    void Update () {
        ActivateBeehives();
        OpenDoor();
	}

    private void OpenDoor()
    {
        int count = 0;
        foreach (Transform beehive in beehives)
        {
            if (beehive.gameObject.activeSelf) count++;
        }
        if (count == beehives.Length)
        {
            door.GetChild(0).gameObject.SetActive(true);
            door.GetChild(1).gameObject.SetActive(false);
        }
    }

    private void ActivateBeehives()
    {
        if (roomBounds.Contains(player1.position) && roomBounds.Contains(player2.position))
        {
            if (!playersInside)
            {
                playersInside = true;
                foreach (Beehive beehiveScript in beehiveScripts)
                {
                    beehiveScript.SpawnBees();
                }
            }
        }
    }

    public void ResetRoom()
    {
        playersInside = false;
        foreach (Transform beehive in beehives)
        {
            beehive.gameObject.SetActive(true);
        }
        foreach (Beehive beehiveScript in beehiveScripts)
        {
            beehiveScript.ResetBeehive();
        }
        door.GetChild(0).gameObject.SetActive(false);
        door.GetChild(1).gameObject.SetActive(true);
    }
}
