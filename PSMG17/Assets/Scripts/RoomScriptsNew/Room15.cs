using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room15 : MonoBehaviour {

    [HideInInspector]
    public bool memoryComplete = false;

    private Transform door;
    private Memory memory;

	// Use this for initialization
	void Start () {
        door = transform.FindChild("Door");
        memory = GetComponentInChildren<Memory>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        OpenDoor();
	}

    private void OpenDoor()
    {
        if (memoryComplete)
        {
            door.GetChild(0).gameObject.SetActive(true);
            door.GetChild(1).gameObject.SetActive(false);
        }
    }

    public void ResetRoom()
    {
        memory.ResetPuzzle();
        memoryComplete = false;

        door.GetChild(0).gameObject.SetActive(false);
        door.GetChild(1).gameObject.SetActive(true);
    }
}
