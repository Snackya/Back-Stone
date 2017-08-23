using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room10 : MonoBehaviour {

    private HubPuzzle hubPuzzle;
    private Transform door;

	void Start () {
        hubPuzzle = GetComponentInChildren<HubPuzzle>();
        door = transform.FindChild("Door");
    }
	
	// Update is called once per frame
	void Update () {
        OpenDoor();
	}

    private void OpenDoor()
    {
        if (hubPuzzle.puzzleCompleted)
        {
            door.GetChild(0).gameObject.SetActive(true);
            door.GetChild(1).gameObject.SetActive(false);
        }
    }
}
