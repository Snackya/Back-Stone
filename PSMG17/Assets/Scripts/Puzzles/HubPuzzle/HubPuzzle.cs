using UnityEngine;

public class HubPuzzle : MonoBehaviour {

    private Transform hubs;

    [HideInInspector]
    public bool puzzleCompleted = false;

    private int totalHubs;

    private float[] rotationTolerance = new float[] { 0.71f, -0.7f };

	void Awake ()
    {
        hubs = GameObject.Find("Hub").transform;
        totalHubs = hubs.childCount;
	}
	
	void Update ()
    {
        CheckIfPuzzleIsCompleted();
	}

    public void CheckIfPuzzleIsCompleted()
    {
        int counter = 0;

        foreach (Transform hub in hubs)
        {
            if (hub.localEulerAngles.z < rotationTolerance[0] && hub.localEulerAngles.z > rotationTolerance[1]) counter++;
        }

        if (counter == totalHubs)
        {
            puzzleCompleted = true;
        }
    }
}
