using UnityEngine;

public class HubPuzzle : MonoBehaviour {

    private Transform hubs;

    [HideInInspector]
    public bool puzzleCompleted = false;

    private int totalHubs;

	void Awake ()
    {
        hubs = GameObject.Find("Hub").transform;
        totalHubs = hubs.childCount;
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
            if (hub.rotation.z < 0.1) counter++;
        }

        if (counter == totalHubs)
        {
            puzzleCompleted = true;
            Debug.Log("Puzzle completed. Open gate, etc.");
        }
    }
}
