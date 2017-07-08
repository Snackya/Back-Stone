using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubPuzzle : MonoBehaviour {

    private Transform hubs;

    private int totalHubs = 4;

	void Awake ()
    {
        hubs = GameObject.Find("Hub").transform;
        Debug.Log(hubs);
        Debug.Log(hubs.childCount);
	}
	
	void Update ()
    {
        CheckIfPuzzleIsCompleted();
	}

    private void CheckIfPuzzleIsCompleted()
    {
        int counter = 0;

        foreach (Transform hub in hubs)
        {
            if (hub.rotation.z == 0) counter++;
        }

        if (counter == totalHubs)
        {
            Debug.Log("Puzzle completed. Open gate, etc.");
        }
    }
}
